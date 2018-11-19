using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StreamsDemo
{
    // C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015
    // Chapter 15: Streams and I/O
    // Chapter 6: Framework Fundamentals - Text Encodings and Unicode
    // https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx

    public static class StreamsExtension
    {
        
        #region Public members

        #region TODO: Implement by byte copy logic using class FileStream as a backing store stream .

        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int count = 0;

            byte[] bytes = File.ReadAllBytes(sourcePath);

            using(FileStream fileGetter = new FileStream(destinationPath, FileMode.Create, FileAccess.ReadWrite))
            {
                foreach (var oneByte in bytes)
                {
                    fileGetter.WriteByte(oneByte);
                    count++;
                }
            }

            return count;
        }

        #endregion

        #region TODO: Implement by byte copy logic using class MemoryStream as a backing store stream.

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            // TODO: step 1. Use StreamReader to read entire file in string

            string data;

            using (var reader = new StreamReader(sourcePath, Encoding.UTF8))
            {
                data = reader.ReadToEnd();
            }

            // TODO: step 2. Create byte array on base string content - use  System.Text.Encoding class


            byte[] bytes = Encoding.UTF8.GetBytes(data);
            // TODO: step 3. Use MemoryStream instance to read from byte array (from step 2)

            var memoryStream = new MemoryStream(bytes);

            // TODO: step 4. Use MemoryStream instance (from step 3) to write it content in new byte array

            bytes = new byte[1];
            byte[] bytesSaver = new byte[memoryStream.Length];
            int count = 0;

            while(memoryStream.Read(bytes, 0, 1) > 0)
            {
                bytesSaver[count] = bytes[0];
                count++;
            }

            memoryStream.Dispose();

            // TODO: step 5. Use Encoding class instance (from step 2) to create char array on byte array content

            char[] charArray = Encoding.UTF8.GetChars(bytesSaver);

            // TODO: step 6. Use StreamWriter here to write char array content in new file

            count = 0;
            using (var streamWriter = new StreamWriter(destinationPath))
            {
                foreach (var item in charArray)
                {
                    streamWriter.Write(item);
                    count++;
                }
            }

            return count;
        }

        #endregion

        #region TODO: Implement by block copy logic using FileStream buffer.

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int count = 0;
            const int DEFAULT_BUFFER = 100;
            byte[] bytes = new byte[DEFAULT_BUFFER];

            using (FileStream fileWriter = new FileStream(sourcePath, FileMode.Open))
            {
                using (FileStream fileGetter = new FileStream(destinationPath, FileMode.Create))
                {
                    while (fileWriter.Read(bytes, 0, bytes.Length) > 0)
                    {  
                        fileGetter.Write(bytes, 0, bytes.Length);
                    }

                    count = (int)fileGetter.Length;
                }
            }

            return count;
        }

        #endregion

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            byte[] bytes;

            using (var reader = new StreamReader(sourcePath, Encoding.UTF8))
            {
                bytes = Encoding.UTF8.GetBytes(reader.ReadToEnd()); 
            }

            var memoryStream = new MemoryStream(bytes);
            var streamWriter = new StreamWriter(destinationPath);

            const int BUFFER_SIZE = 100;
            bytes = new byte[BUFFER_SIZE];
            int count = 0;

            while (memoryStream.Read(bytes, 0, BUFFER_SIZE) > 0)
            {
                streamWriter.Write(Encoding.UTF8.GetChars(bytes));
                count += BUFFER_SIZE;
            }

            memoryStream.Dispose();
            streamWriter.Dispose();

            return count;
        }

        #endregion

        #region TODO: Implement by block copy logic using class-decorator BufferedStream.

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            const int BUFFER_SIZE = 100;
            int count = 0;

            var stream = new FileStream(sourcePath, FileMode.Open);
            var streamGetter = new FileStream(destinationPath, FileMode.OpenOrCreate);
            var bufferStream = new BufferedStream(stream, BUFFER_SIZE);

            byte[] bytes = new byte[BUFFER_SIZE];

            while(bufferStream.Read(bytes, 0, BUFFER_SIZE) > 0)
            {
                streamGetter.Write(bytes, 0, BUFFER_SIZE);
                count += BUFFER_SIZE;
            }

            streamGetter.Dispose();
            bufferStream.Dispose();

            return count;
        }

        #endregion

        #region TODO: Implement by line copy logic using FileStream and classes text-adapters StreamReader/StreamWriter

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            string[] buffer = File.ReadAllLines(sourcePath);

            using(var stream = new StreamWriter(destinationPath))
            {
                foreach (var item in buffer)
                {
                    stream.WriteLine(item);
                }
            }

            return buffer.Length;
        }

        #endregion

        #region TODO: Implement content comparison logic of two files 

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            byte[] bytes1 = File.ReadAllBytes(sourcePath);
            byte[] bytes2 = File.ReadAllBytes(destinationPath);

            if(bytes1.Length != bytes2.Length)
            {
                return false;
            }

            for (int i = 0; i < bytes1.Length; i++)
            {
                if(bytes1[i] != bytes2[i])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #endregion

        #region Private members

        #region TODO: Implement validation logic

        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentException($"{nameof(sourcePath)} can't be equal to null or empty.");
            }

            if (!File.Exists(sourcePath))
            {
                throw new ArgumentException($"{nameof(sourcePath)} isn't exist.");
            }
        }

        #endregion

        #endregion

    }
}

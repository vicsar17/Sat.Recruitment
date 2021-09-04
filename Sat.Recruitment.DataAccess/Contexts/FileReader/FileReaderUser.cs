﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.DataAccess.Contexts
{
    internal class FileReaderUser
    {
        private const string PathFileUsers = "/Files/Users.txt";

        public StreamReader GetStreamReaderUser()
        {
            var path = Directory.GetCurrentDirectory() + PathFileUsers;
            var fileStream = new FileStream(path, FileMode.Open);
            return new StreamReader(fileStream);
        }
    }
}

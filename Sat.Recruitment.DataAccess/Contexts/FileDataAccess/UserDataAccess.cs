using Sat.Recruitment.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.DataAccess.Contexts
{
    public class UserDataAccess : IUserDataAccess
    {
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var userList = new List<User>();
            using (var readerUser = new FileReaderUser().GetStreamReaderUser())
            {
                string currentLine;
                while ((currentLine = await readerUser.ReadLineAsync()) != null)
                {
                    var currentLineArray = currentLine.Split(',');
                    var newUser = new User
                    {
                        Name = currentLineArray[0],
                        Email = currentLineArray[1],
                        Phone = currentLineArray[2],
                        Address = currentLineArray[3],
                        UserType = currentLineArray[4],
                        Money = ConvertToDecimal(currentLineArray[5]),
                    };
                    userList.Add(newUser);
                }
            }
            return userList;
        }

        private decimal ConvertToDecimal(string value) => decimal.Parse(value);
        
    }
}

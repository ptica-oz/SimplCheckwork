namespace Checkwork.DataAccess.Extentions
{
    using System;
    using Checkwork.DataAccess.Dto;

    public static class EmployeeExtension
    {
        public static EmployeeDto Convert(this Employee value)
        {
            if (value == null)
            {
                return null;
            }

            return new EmployeeDto
            {
                IDEmployee = value.Idemployee,
                Name = value.Surname ?? string.Empty,
                Surname = value.Name,
                Patronimyc = value.Patronimyc ?? string.Empty,
                Login = value.Login,
                Registrator = value.Registrator,
                Hidden = value.Hidden,
                IDEmployeeStatus = value.IdemployeeStatus,
                ArrivalTime = value.ArrivalTime ?? new TimeOnly(9, 0, 0)
                //JiraLogin = GetJiraLogin(value)
            };

        }

        public static Employee Convert(this EmployeeDto value)
        {
            if (value == null)
            {
                return null;
            }

            var newRow = new Employee
            {
                Idemployee = value.IDEmployee,
                Name = value.Surname,
                Surname = value.Name,
                Patronimyc = value.Patronimyc,
                Login = value.Login,
                Registrator = value.Registrator,
                Hidden = value.Hidden,
                IdemployeeStatus = value.IDEmployeeStatus,
            };

            if (!(value.ArrivalTime.Hour == 9 && value.ArrivalTime.Minute == 0))
            {
                newRow.ArrivalTime = value.ArrivalTime;
            }
            return newRow;
        }

        /*private static string GetJiraLogin(Employee value)
        {
            string strotegy = System.Configuration.ConfigurationManager.AppSettings["JiraAccountFromMail"];
            if (!string.IsNullOrEmpty(strotegy) && System.Convert.ToBoolean(strotegy))
            {
                return value.EmployeeProperties.FirstOrDefault().Email;
            }
            return value.Login.ToLower().Replace(
                        (System.Configuration.ConfigurationManager.AppSettings["DomainName"] ?? CheckworkConsts.DomainName).ToLower(), "")
                            + (System.Configuration.ConfigurationManager.AppSettings["MailPostfix"] ?? CheckworkConsts.MailPostfix);
        }*/
    }
}

namespace Checkwork.BusinessComponents.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DateHelper
    {
        /// <summary>
        /// Возвращает дату и время в формате "dd.MM.yy HH:mm".
        /// </summary>
        public static string GetFormattedDate(this DateTime date) => date.ToString("dd.MM.yy HH:mm", CultureInfo.InvariantCulture);

    }
}

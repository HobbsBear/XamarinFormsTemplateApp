using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassTimeTools
{
	class DateTimeConverter
	{
		public static DateTimeOffset convertDateTimeToDateTimeOffset(DateTime dateTime)
		{
			return new DateTimeOffset(dateTime);
		}
	}
}

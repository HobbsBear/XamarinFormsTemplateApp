using PassTimeSportsService.DataObjects;
using System.Linq;

namespace PassTimeSportsService.Extensions
{
	public static class TodoItemExtensions
	{
		public static IQueryable<TodoItem> PerUserFilter(this IQueryable<TodoItem> query, string userid)
		{
			return query.Where(item => item.UserId.Equals(userid));
		}
	}
}
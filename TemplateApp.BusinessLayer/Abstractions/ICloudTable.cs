using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateApp.Abstractions
{
	//The ICloudTable<T> interface defines the normal CRUD operations asynchronously
	//ReadAllItemsAsync() is a method that returns a collection of all the items

	public interface ICloudTable<T> where T : TableData
	{
		Task<T> CreateItemAsync(T item);
		Task<T> ReadItemAsync(string id);
		Task<T> UpdateItemAsync(T item);
		Task DeleteItemAsync(T item);
		Task<ICollection<T>> ReadAllItemsAsync();
	}
}

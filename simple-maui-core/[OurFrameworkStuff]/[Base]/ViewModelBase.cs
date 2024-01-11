#region Using Statements
using System.Runtime.CompilerServices;
#endregion Using Statements

namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Provides a base class to be extended by all view models.
	/// </summary>
	/// <remarks>
	/// This base class is intended to provide an implementation of functionality common to all view models and any tie in to Xamarin Forms 
	/// should be excluded from this base class.  For example, we have some view models that may simply represent an item in a list displayed on a page
	/// and those view models do not need all the overhead and extra functionality that a view model backing a Xamarin Forms content page may.
	/// </remarks>
	/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
	/// <seealso cref="ExtendedBindableObjectBase"/>
	public abstract class ViewModelBase : ExtendedBindableObjectBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelBase"/> class.
		/// </summary>
		protected ViewModelBase()
		{
		}

		#endregion Constructors

		#region INotifyPropertyChanged support

		/// <summary>
		/// Holds the backing values for the data-bound properties in the view model. 
		/// </summary>
		private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

		/// <summary>
		/// Gets backing value for data-bound property. 
		/// </summary>
		/// <typeparam name="T">The backing value can be of any type, so using a generic here.</typeparam>
		/// <param name="propertyName">The data-bound property name.</param>
		/// <returns>Returns the backing value, which can be of any type, so using a generic here.</returns>
		protected T Get<T>([CallerMemberName] string propertyName = null)
		{
			if (_properties.TryGetValue(propertyName, out object value))
			{
				return value == null ? default : (T)value;
			}

			return default;
		}

		/// <summary>
		/// Sets the data-bound properties backing value. 
		/// </summary>
		/// <typeparam name="T">The backing value can be of any type, so using a generic here.</typeparam>
		/// <param name="value">The value submitted to be the backing value.</param>
		/// <param name="propertyName">The name of the data-bound property.</param>
		/// <returns>Returns true/false if the set was successful. Returns false if no change was made.</returns>
		protected bool Set<T>(T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(Get<T>(propertyName), value))
			{
				return false;
			}

			_properties[propertyName] = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		#endregion INotifyPropertyChanged support		
	}
}

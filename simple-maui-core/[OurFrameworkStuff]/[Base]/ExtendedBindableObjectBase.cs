#region Using Statements
using System.Linq.Expressions;
using System.Reflection;
#endregion

namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Generics extended bindable object with raise property changed methods
	/// </summary>
	/// <seealso cref="BindableObject" />
	public abstract class ExtendedBindableObjectBase : BindableObject
	{
		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		/// <param name="property">The property.</param>
		public void RaisePropertyChanged<T>(Expression<Func<T>> property)
		{
			string name = GetMemberInfo(property).Name;
			OnPropertyChanged(name);
		}

		/// <summary>
		/// Gets the member information.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <returns>the member info</returns>
		private MemberInfo GetMemberInfo(Expression expression)
		{
			MemberExpression operand;
			LambdaExpression lambdaExpression = (LambdaExpression)expression;
			if (lambdaExpression.Body as UnaryExpression != null)
			{
				UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
				operand = (MemberExpression)body.Operand;
			}
			else
			{
				operand = (MemberExpression)lambdaExpression.Body;
			}

			return operand.Member;
		}
	}
}

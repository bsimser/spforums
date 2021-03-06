using System;
using System.Collections;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections
{
	/*
	 * 
	 * CollectionBase tips from 
	 * http://www.mattberther.com/2004/09/000540.html
	 * 
	 * Also important is to mark your new collection with the Serializable attribute. 
	 * The Serializable attribute is applied to the CollectionBase internal class, 
	 * however, this attribute is not inherited. It's easier to do this now rather than 
	 * later when you find a serialization bug that has manifested itself in some strange 
	 * way. Trust me, just add the attribute.
	 * 
	 * A lot of times, you 'l l want to return a ReadOnly collection from one of your 
	 * properties. For example, say you'r e returning a list of States to populate a state 
	 * list. You will not want to allow the user to modify this collection once it has been 
	 * generated by your object. You can accomplish this and still use CollectionBase.
	*/

	[Serializable]
	public class ObjectCollection : CollectionBase
	{
		public ObjectCollection()
		{
		}

		public ObjectCollection(ObjectCollection coll)
		{
			this.InnerList.AddRange(coll);
		}

		public object this[int index]
		{
			get { return (object) List[index]; }
			set { List[index] = value; }
		}

		public virtual void Add(object person)
		{
			List.Add(person);
		}

		public virtual void Remove(object person)
		{
			List.Remove(person);
		}

		public bool Contains(object person)
		{
			return List.Contains(person);
		}

		public int IndexOf(object person)
		{
			return List.IndexOf(person);
		}

		public static ObjectCollection ReadOnly(ObjectCollection coll)
		{
			return new ReadOnlyPersonCollection(coll);
		}

		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
//			if (!(value is <YourObjectType>))
//			{
//				throw new ArgumentException("Collection only supports object objects.");
//			}
		}

		#region ReadOnlyPersonCollection

		/*
		 * We added a static method called ReadOnly, which takes a PersonCollection and returns 
		 * a PersonCollection. Inside of our PersonCollection object, we have a nested internal 
		 * class called ReadOnlyPersonCollection, which derives from PersonCollection and provides 
		 * the implementation that will make the collection read-only. Since ReadOnlyPersonCollection 
		 * derives from PersonCollection, we have no problem returning it from our static
		 * ReadOnly method.
		 * 
		 * We see here that our ReadOnlyPersonCollection is also overriding a few additional methods. 
		 * CollectionBase.Clear is not marked as virtual, so thankfully, Microsoft has provided us a 
		 * way to stop the Clear from occurring. Prior to the list.Clear() call, OnClear() is called. 
		 * Similar functionality is available for insert, update and delete. These methods are all 
		 * overridden, since a read-only collection should not be able to be modified in any way.
		 * 
		 * Of course, .NET 2.0, with the introduction of generics, will render all of this 
		 * information invalid.
		*/

		private sealed class ReadOnlyPersonCollection : ObjectCollection
		{
			private const string ERROR_STRING = "Collection is read-only.";

			internal ReadOnlyPersonCollection(ObjectCollection coll) : base(coll)
			{
			}

			public override void Add(object person)
			{
				throw new NotSupportedException(ERROR_STRING);
			}

			public override void Remove(object person)
			{
				throw new NotSupportedException(ERROR_STRING);
			}

			protected override void OnClear()
			{
				throw new NotSupportedException(ERROR_STRING);
			}

			protected override void OnInsert(int index, object value)
			{
				throw new NotSupportedException(ERROR_STRING);
			}

			protected override void OnRemove(int index, object value)
			{
				throw new NotSupportedException(ERROR_STRING);
			}

			protected override void OnSet(int index, object oldValue, object newValue)
			{
				throw new NotSupportedException(ERROR_STRING);
			}
		}

		#endregion
	}
}
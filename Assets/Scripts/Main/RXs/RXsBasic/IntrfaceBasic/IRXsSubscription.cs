using System;

namespace Main.RXs
{
	public interface IRXsSubscription : IDisposable
	{
		void Subscribe();
		void Unsubscribe();
	}
}
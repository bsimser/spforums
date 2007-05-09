using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace BilSimser.SharePoint.WebParts.Forums.Utility
{
	/// <summary>
	/// A timer class that tracks the time (in milliseconds)
	/// it takes to load a page.
	/// </summary>
	internal class ForumTimer
	{
		#region Private Methods

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);
		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
		
		#endregion

		#region Fields

		private long freq;
		private long startTime, stopTime;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ForumTimer"/> class.
		/// </summary>
		/// <param name="startTimer">if set to <c>true</c> [b start].</param>
		public ForumTimer(bool startTimer)
		{
			startTime = 0;
			stopTime = 0;

			if (QueryPerformanceFrequency(out freq) == false)
			{
				// high-performance counter not supported
				throw new Win32Exception();
			}

			if (startTimer)
				Start();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Stops the timer.
		/// </summary>
		public void Stop()
		{
			QueryPerformanceCounter(out stopTime);
		}

		/// <summary>
		/// Starts the timer.
		/// </summary>
		public void Start()
		{
			// lets do the waiting threads there work
			Thread.Sleep(0);
			QueryPerformanceCounter(out startTime);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the duration (in seconds).
		/// </summary>
		/// <value>The duration.</value>
		public double Duration
		{
			get { return (double) (stopTime - startTime)/(double) freq; }
		}

		#endregion
	}
}
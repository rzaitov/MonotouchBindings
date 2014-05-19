using System;

namespace VK
{
	public enum VKOperationState
	{
		PausedState = -1,
		ReadyState = 1,
		ExecutingState = 2,
		FinishedState = 3
	}

	public enum VKImageType
	{
		Jpg,
		Png
	}
}
using System.Collections.Generic;

public class Subject
{
	List<IGameObserver> _obvs = new List<IGameObserver>();

	public void AddObvs(IGameObserver toAdd)
	{
		_obvs.Add(toAdd);
	}

	public void RemoveObvs(IGameObserver toRemove)
	{
		_obvs.Remove(toRemove);
	}

	public void Notify()
	{
		foreach(var obv in _obvs)
		{
			obv.OnNotify();
		}
	}
}
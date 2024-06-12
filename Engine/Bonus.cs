using System.ComponentModel;

namespace Engine
{
	public class Bonus(string name, string description)
	{
		public string Name { get; private set; } = name;
		public string Description { get; private set; } = description;
		public int DurationMS { get; protected set; } = 0;
		public Action? OnCompleted { get; set; } = null;

		public virtual void Use(LevelManager lm)
		{
			Console.WriteLine(Name);
			throw new NotImplementedException();
		}
	}

	public class BonusPair<B>(B? first, B? second) : INotifyPropertyChanged where B : Bonus
	{
		public B? First { get; set; } = first;
		public B? Second { get; set; } = second;
		public int FrontEndValue { get; private set; } = 0;
		public event PropertyChangedEventHandler? PropertyChanged;
		public bool IsEmpty() => First == null && Second == null;

		public void Clear()
		{
			First = null;
			Second = null;
			FrontEndValue = 0;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FrontEndValue)));
		}

		// To be used if the user wants to use a specific bonus.
		// If position 1 is chosen, the second bonus will be placed as number
		// one.
		public void Use(LevelManager lm, uint position = 1)
		{
			if (IsEmpty()) return;
			if (position == 1) UseFirst(lm);
			else if (position == 2)
			{
				Second?.Use(lm);
				Second = null;
			}

			CalculateFrontEndValue();
		}

		public void UseFirst(LevelManager lm)
		{
			// When the first bonus is used, move second as first.
			if (IsEmpty()) return;
			First?.Use(lm);
			First = Second;
			Second = null;

			CalculateFrontEndValue();
		}

		public void Add(B bonus)
		{
			if (First == null)
				First = bonus;
			else Second ??= bonus;

			CalculateFrontEndValue();
		}

		public void CalculateFrontEndValue()
		{
			int tensDigit = (First != null) ? (First is HealthBonus ? 1 : 2) : 0;
			int unitsDigit = (Second != null) ? (Second is HealthBonus ? 1 : 2) : 0;
			FrontEndValue = tensDigit * 10 + unitsDigit;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FrontEndValue)));
		}
	}

	public sealed class HealthBonus : Bonus
	{
		public HealthBonus() : base("HealthBonus", "HealthBonus") { }

		public override void Use(LevelManager lm)
		{
			// See how we could make it a bit more challenging.
			lm.Health.IncrementHealth();
			OnCompleted?.Invoke();
		}
	}


	public sealed class TorchBonus : Bonus
	{
		public TorchBonus() : base("TorchBonus", "TorchBonus")
		{
		}
	}

}

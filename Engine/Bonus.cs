using System.ComponentModel;

namespace Engine
{
	/// <summary>
	/// Classe de base représentant un bonus dans le jeu.
	/// </summary>
	public class Bonus(string name, string description)
	{
		/// <summary>
		/// Nom du bonus.
		/// </summary>
		public string Name { get; private set; } = name;
		/// <summary>
		/// Description du bonus.
		/// </summary>
		public string Description { get; private set; } = description;

		/// <summary>
		/// Action à exécuter lorsque le bonus est terminé (optionnel).
		/// </summary>
		public Action? OnCompleted { get; set; } = null;

		/// <summary>
		/// Méthode virtuelle pour utiliser le bonus.
		/// Peut être surchargée dans les classes dérivées pour implémenter des effets spécifiques.
		/// </summary>
		/// <param name="lm">Le gestionnaire de niveau.</param>
		public virtual void Use(LevelManager lm) { }
	}

	/// <summary>
	/// Classe représentant une paire de bonus et gérant leur utilisation.
	/// </summary>
	/// <typeparam name="B">Type de bonus (doit hériter de la classe Bonus).</typeparam>
	public class BonusPair<B> : INotifyPropertyChanged where B : Bonus
	{
		/// <summary>
		/// Premier bonus de la paire.
		/// </summary>
		public B? First { get; set; }

		/// <summary>
		/// Deuxième bonus de la paire.
		/// </summary>
		public B? Second { get; set; }

		/// <summary>
		/// Valeur utilisée pour l'affichage dans l'interface utilisateur.
		/// </summary>
		public int FrontEndValue { get; private set; } = 0;

		// Événement pour notifier les changements de propriété (utilisé pour mettre à jour l'interface)
		public event PropertyChangedEventHandler? PropertyChanged;

		/// <summary>
		/// Indique si la paire de bonus est vide (ne contient aucun bonus).
		/// </summary>
		/// <returns>True si la paire est vide, false sinon.</returns>
		public bool IsEmpty() => First == null && Second == null;

		/// <summary>
		/// Constructeur de la classe BonusPair.
		/// </summary>
		/// <param name="first">Le premier bonus.</param>
		/// <param name="second">Le deuxième bonus.</param>
		public BonusPair(B? first, B? second)
		{
			First = first;
			Second = second;
		}

		/// <summary>
		/// Constructeur de la classe BonusPair à partir d'une valeur pour l'interface.
		/// </summary>
		/// <param name="value">Valeur représentant les bonus.</param>
		public BonusPair(int value)
		{
			FrontEndValue = value;
			if (value > 0)
			{
				First = CreateBonus(value / 10); // Crée le premier bonus à partir du chiffre des dizaines
				Second = CreateBonus(value % 10); // Crée le deuxième bonus à partir du chiffre des unités
			}
			else
			{
				First = null;
				Second = null;
			}

			CalculateFrontEndValue(); // Met à jour la valeur pour l'interface utilisateur
		}

		/// <summary>
		/// Crée un bonus en fonction d'une valeur numérique.
		/// </summary>
		/// <param name="value">La valeur représentant le type de bonus (1 pour HealthBonus, 2 pour TorchBonus).</param>
		/// <returns>Le bonus créé, ou null si la valeur est invalide.</returns>
		public B? CreateBonus(int value)
		{
			return value == 1 ? (B)(object)new HealthBonus() : value == 2 ? (B)(object)new TorchBonus() : null;
		}

		/// <summary>
		/// Vide la paire de bonus.
		/// </summary>
		public void Clear()
		{
			First = null;
			Second = null;
			FrontEndValue = 0;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FrontEndValue))); // Notifie le changement de valeur
		}

		/// <summary>
		/// Utilise le premier bonus de la paire.
		/// </summary>
		/// <param name="lm">Le gestionnaire de niveau.</param>
		public void UseFirst(LevelManager lm)
		{
			if (IsEmpty()) return; // Ne fait rien si la paire est vide

			First?.Use(lm);  // Utilise le premier bonus (s'il n'est pas null)
			First = Second;   // Déplace le deuxième bonus à la première position
			Second = null;   // Réinitialise le deuxième bonus

			CalculateFrontEndValue(); // Met à jour la valeur pour l'interface utilisateur
		}

		/// <summary>
		/// Ajoute un bonus à la paire.
		/// </summary>
		/// <param name="bonus">Le bonus à ajouter.</param>
		public void Add(B bonus)
		{
			if (First == null)
				First = bonus;         // Ajoute au premier emplacement s'il est vide
			else Second ??= bonus;   // Ajoute au deuxième emplacement s'il est vide

			CalculateFrontEndValue(); // Met à jour la valeur pour l'interface utilisateur
		}

		/// <summary>
		/// Calcule la valeur pour l'interface utilisateur en fonction des bonus présents.
		/// </summary>
		public void CalculateFrontEndValue()
		{
			// Calcule le chiffre des dizaines (1 pour HealthBonus, 2 pour TorchBonus)
			int tensDigit = (First != null) ? (First is HealthBonus ? 1 : 2) : 0;

			// Calcule le chiffre des unités
			int unitsDigit = (Second != null) ? (Second is HealthBonus ? 1 : 2) : 0;

			// Combine les chiffres pour obtenir la valeur finale
			FrontEndValue = tensDigit * 10 + unitsDigit;

			// Notifie le changement de valeur
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FrontEndValue)));
		}
	}

	// Bonus de santé : augmente la santé du joueur lorsqu'il est utilisé.
	public sealed class HealthBonus : Bonus
	{
		public HealthBonus() : base("HealthBonus", "HealthBonus") { } // Constructeur qui appelle le constructeur de base

		public override void Use(LevelManager lm) // Surcharge de la méthode Use pour l'effet du bonus de santé
		{
			lm.Health.IncrementHealth();  // Incrémente la santé du joueur
			OnCompleted?.Invoke();        // Déclenche l'événement OnCompleted si défini (pour signaler la fin du bonus)
		}
	}

	// Bonus de torche (non implémenté dans cet exemple)
	public sealed class TorchBonus : Bonus
	{
		public TorchBonus() : base("TorchBonus", "TorchBonus") { } // Constructeur qui appelle le constructeur de base
	}


}

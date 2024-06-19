using System.ComponentModel; // Utilisation du namespace pour l'interface INotifyPropertyChanged

namespace Engine
{
	/// <summary>
	/// Classe représentant la santé d'un personnage dans le jeu.
	/// Elle gère les vies, les points de vie et notifie les changements via INotifyPropertyChanged.
	/// </summary>
	public class Health(byte lives, byte healthPoints) : INotifyPropertyChanged
	{
		private static readonly byte _defaultLives = 3;  // Nombre de vies par défaut
		private static readonly byte _defaultHP = 2;     // Points de vie par défaut par vie

		// Propriété pour le nombre de vies, accessible en lecture seule
		public byte Lives { get; private set; } = lives;

		// Propriété pour les points de vie, accessible en lecture seule
		public byte HealthPoints { get; private set; } = healthPoints;

		// Événement déclenché lorsqu'une propriété change (requis pour INotifyPropertyChanged)
		public event PropertyChangedEventHandler? PropertyChanged;

		// Constructeur par défaut : initialise la santé avec les valeurs par défaut
		public Health() : this(_defaultLives, _defaultHP) { }

		/// <summary>
		/// Réduit la santé du personnage.
		/// Si les points de vie atteignent 0, une vie est perdue et les points de vie sont réinitialisés.
		/// </summary>
		public void ReduceHealth()
		{
			if (!IsDead() && HealthPoints <= 0) // Si le personnage n'est pas mort et n'a plus de points de vie dans sa vie actuelle
			{
				Lives--;             // Réduire le nombre de vies
				HealthPoints = _defaultHP; // Réinitialiser les points de vie pour la nouvelle vie
			}
			else
				HealthPoints--; // Réduire les points de vie

			// Notifier que la propriété HealthPoints a changé
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HealthPoints)));
		}

		/// <summary>
		/// Augmente la santé du personnage jusqu'au maximum de points de vie par vie.
		/// </summary>
		public void IncrementHealth()
		{
			if (HealthPoints < _defaultHP) // Si les points de vie ne sont pas au maximum
				HealthPoints++;           // Augmenter les points de vie

			// Notifier que la propriété HealthPoints a changé
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HealthPoints)));
		}

		/// <summary>
		/// Réinitialise la santé du personnage aux valeurs par défaut.
		/// </summary>
		public void ResetHealth()
		{
			Lives = _defaultLives;
			HealthPoints = _defaultHP;
		}

		/// <summary>
		/// Vérifie si le personnage est mort (plus de vies et plus de points de vie).
		/// </summary>
		/// <returns>True si le personnage est mort, false sinon.</returns>
		public bool IsDead() => Lives <= 0 && HealthPoints <= 0;
	}
}

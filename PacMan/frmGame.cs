using Engine;
using Engine.utils;

namespace PacMan
{
	// Formulaire de jeu 
	// Ce fichier est divisé en deux parties : frmGame.cs(fichier actuel) et frmGamePart.cs
	public partial class frmGame : Form
	{
		public GameManager gameManager { get; private set; } // Propriété pour accéder au gestionnaire de jeu

		private Direction currentDirection = Direction.STOP; // Direction actuelle de Pac-Man (initialement à l'arrêt)

		// Coordonnées précédentes des fantômes et du joueur
		private CellCoordinates CainPrevPos, WinstonPrevPos, ViggoPrevPos, MarquisPrevPos, JohnPrevPos;

		// Dictionnaire pour stocker les images des personnages (John et les fantômes)
		private Dictionary<string, Image[]> characterImages;
		private int animationFrame = 0; // Indice de l'image d'animation actuelle

		// Images des différents éléments du jeu
		private readonly Image Wall = Properties.Resources.wall;
		private readonly Image Coin = Properties.Resources.coin;
		private readonly Image Key = Properties.Resources.Key;
		private readonly Image HealthKit = Properties.Resources.MedKit;
		private readonly Image Torch = Properties.Resources.torch;
		private readonly Image Empty = Properties.Resources.noRessources;

		// Formulaires de pause et de notification
		private readonly FrmPause frmPause;
		private readonly FrmNotif frmNotif;

		public frmGame(GameMode gameMode, string playerName)
		{
			// Initialisation de la fenêtre de jeu
			InitializeComponent();

			// Initialisation du dictionnaire d'images des personnages
			characterImages = [];

			// Création du gestionnaire de jeu
			gameManager = new GameManager(gameMode, playerName, Properties.Settings.Default.PlayerUID);
			frmPause = new FrmPause(gameManager);
			frmNotif = new FrmNotif(this); // Création du formulaire de notification
		}

		// Constructeur pour reprendre une partie sauvegardée
		public frmGame(int sessionId)
		{
			// Initialisation de la fenêtre de jeu
			InitializeComponent();
			characterImages = [];

			// Création du gestionnaire de jeu à partir d'une session sauvegardée
			gameManager = new GameManager(sessionId);
			frmPause = new FrmPause(gameManager);
			frmNotif = new FrmNotif(this);
		}

		// Gestionnaire de l'événement de chargement du formulaire
		private void frmGame_Load(object sender, EventArgs e)
		{
			// Délier les sources de données des contrôles
			UnbindDataSources();

			// Charger les images des personnages
			LoadCharacterImages();

			// Lier les sources de données aux contrôles
			BindDataSources();

			// Afficher le labyrinthe
			DisplayMaze(gameManager.LevelManager.LevelMap);
		}

		private void UnbindDataSources()
		{
			// Supprime les liaisons de données existantes pour les contrôles suivants :
			lblScore.DataBindings.Clear();      // Label affichant le score
			pnlKeys.DataBindings.Clear();       // Panneau affichant les images des clés
			pnlHealth.DataBindings.Clear();     // Panneau affichant les points de vie
			PnlBonuses.DataBindings.Clear();   // Panneau affichant les bonus
		}

		private void BindDataSources()
		{
			// Ajoute des liaisons de données entre les contrôles et les propriétés du GameManager :
			lblScore.DataBindings.Add("Text", gameManager.LevelManager, "Score");           // Lie le texte du label au score
			pnlKeys.DataBindings.Add("TabIndex", gameManager.LevelManager, "Key");         // Lie le tabindex du panneau au nombre de clés
			pnlHealth.DataBindings.Add("TabIndex", gameManager.LevelManager.Health, "HealthPoints");   // Lie le tabindex au nombre de points de vie
			PnlBonuses.DataBindings.Add("TabIndex", gameManager.LevelManager.Player.Bonuses, "FrontEndValue");  // Lie le tabindex à la valeur des bonus
		}

		// Recharge le formulaire de jeu (utile pour mettre à jour l'affichage après un changement de niveau)
		public void ReloadForm()
		{
			this.frmGame_Load(this, EventArgs.Empty); // Appelle la méthode de chargement du formulaire avec un événement vide
		}

		// Gestionnaire de l'événement de fermeture du formulaire de jeu
		private void frmGame_Closed(object sender, EventArgs e)
		{
			// Affiche le premier formulaire ouvert dans l'application (le menu principal)
			Application.OpenForms[0]?.Show();
		}

		// Charge les images des personnages (John et fantômes)
		private void LoadCharacterImages()
		{
			// Crée un dictionnaire associant le nom du personnage et la direction à un tableau d'images pour l'animation
			characterImages = new Dictionary<string, Image[]>
			{
				// Images de John (Espion) pour chaque direction et pour l'arrêt
				{"PlayerUP", [Properties.Resources.Espion_LookUpWalk_Frame1, Properties.Resources.Espion_LookUpWalk_Frame2]},
				{"PlayerDOWN", [Properties.Resources.Espion_LookDownWalk_Frame1, Properties.Resources.Espion_LookDownWalk_Frame2]},
				{"PlayerLEFT", [Properties.Resources.Espion_LookLeftWalk_Frame1, Properties.Resources.Espion_LookLeftWalk_Frame2]},
				{"PlayerRIGHT", [Properties.Resources.Espion_LookRightWalk_Frame1, Properties.Resources.Espion_LookRightWalk_Frame2]},

				// Images des fantômes (Cain, Winston, Viggo, Marquis) pour chaque direction et pour l'arrêt
				{"PlayerStop", [Properties.Resources.Espion_LookDownStop] },
				{"CainSTOP", [Properties.Resources.CainFaceDown1, Properties.Resources.CainFaceDown2] },
				{"WinstonSTOP", [Properties.Resources.WinstonDown1, Properties.Resources.WinstonDown2] },
				{"ViggoSTOP", [Properties.Resources.ViggoDown1, Properties.Resources.ViggoDown2] },
				{"MarquisSTOP", [Properties.Resources.MarquisDown1, Properties.Resources.MarquisDown2] },
				{"CainUP", [Properties.Resources.CainFaceUp1, Properties.Resources.CainFaceUp2] },
				{"CainDOWN", [Properties.Resources.CainFaceDown1, Properties.Resources.CainFaceDown2] },
				{"CainLEFT", [Properties.Resources.CainLeft1, Properties.Resources.CainLeft2] },
				{"CainRIGHT", [Properties.Resources.CainRight1, Properties.Resources.CainRight2] },
				{"WinstonUP", [Properties.Resources.WinstonUp1, Properties.Resources.WinstonUp2] },
				{"WinstonDOWN", [Properties.Resources.WinstonDown1, Properties.Resources.WinstonDown2] },
				{"WinstonLEFT", [Properties.Resources.Winstonleft1, Properties.Resources.WinstonLeft2] },
				{"WinstonRIGHT", [Properties.Resources.WinstonRight1, Properties.Resources.WinstonRight2] },
				{"ViggoUP", [Properties.Resources.ViggoUp1, Properties.Resources.ViggoUp2] },
				{"ViggoDOWN", [Properties.Resources.ViggoDown1, Properties.Resources.ViggoDown2] },
				{"ViggoLEFT", [Properties.Resources.ViggoLeft1, Properties.Resources.ViggoLeft2] },
				{"ViggoRIGHT", [Properties.Resources.ViggoRight1, Properties.Resources.ViggoRight2] },
				{"MarquisUP", [Properties.Resources.MarquisUp1, Properties.Resources.MarquisUp2] },
				{"MarquisDOWN", [Properties.Resources.MarquisDown1, Properties.Resources.MarquisDown2] },
				{"MarquisLEFT", [Properties.Resources.MarquisLeft1, Properties.Resources.MarquisLeft2] },
				{"MarquisRIGHT", [Properties.Resources.MarquisRight1, Properties.Resources.MarquisRight2] }
			};
		}

		private void HandleKeyInput(Keys key)
		{
			// Dictionnaire associant les touches du clavier aux directions du personnage
			var keyToDirectionMap = new Dictionary<Keys, Direction>
			{
				{ Keys.Z, Direction.UP },      // Z ou flèche haut pour aller vers le haut
				{ Keys.Up, Direction.UP },
				{ Keys.Down, Direction.DOWN },  // Flèche bas pour aller vers le bas
				{ Keys.S, Direction.DOWN },
				{ Keys.Left, Direction.LEFT },  // Flèche gauche pour aller vers la gauche
				{ Keys.Q, Direction.LEFT },
				{ Keys.Right, Direction.RIGHT }, // Flèche droite pour aller vers la droite
				{ Keys.D, Direction.RIGHT }
			};

			// Si la touche pressée est valide, met à jour la direction actuelle
			if (keyToDirectionMap.TryGetValue(key, out Direction direction))
				currentDirection = direction;
		}

		// Surcharge de la méthode ProcessCmdKey pour intercepter certaines touches du clavier
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.P) // Si la touche "P" est appuyée
			{
				btnPause_Click(this, EventArgs.Empty); // Simule un clic sur le bouton de pause
				return true;                            // Indique que la touche a été gérée
			}

			if (keyData == Keys.Space) // Si la touche "Espace" est appuyée
			{
				gameManager.LevelManager.Player.Bonuses.UseFirst(gameManager.LevelManager); // Utilise le bonus actif
				return true; // Indique que la touche a été gérée
			}

			HandleKeyInput(keyData); // Appelle la méthode pour gérer la touche en tant que mouvement potentiel
			return base.ProcessCmdKey(ref msg, keyData); // Laisse le traitement par défaut pour les autres touches
		}

		// Gestionnaire de l'événement Tick du timer TmrPlayer (utilisé pour l'animation du jeu)
		private void TmrPlayer_Tick(object sender, EventArgs e)
		{
			// Fait avancer Pac-Man dans la direction actuelle
			gameManager.StepPlayer(currentDirection);

			// Fait avancer les fantômes
			gameManager.StepGhosts();

			// Vérifie si Pac-Man entre en collision avec un fantôme
			if (gameManager.CheckGhostCollisions())
				currentDirection = Direction.STOP;

			// Réaffiche le labyrinthe (mettre à jour les positions des personnages)
			DisplayMaze(gameManager.LevelManager.LevelMap);

			if (gameManager.CheckGameCompleted())
			{
				TmrPlayer.Stop();
				gameManager.Pause();
				FrmGameOver frmGameOver = new(this);
				frmGameOver.ShowDialog(this);
			}
		}

		// Gestionnaire du clic sur le bouton Pause
		private void btnPause_Click(object sender, EventArgs e)
		{
			gameManager.Pause(); // Met le jeu en pause
			frmPause.ShowDialog(this); // Affiche le formulaire de pause
		}

		// Gestionnaire de l'événement de fermeture du formulaire de jeu
		private void frmGame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.ApplicationExitCall) return; // Ne rien faire si la fermeture est due à la fin de l'application

			gameManager.Pause(); // Mettre le jeu en pause
			frmNotif.ShowDialog(this); // Afficher le formulaire de notification pour confirmer la fermeture

			// Si l'utilisateur a choisi de ne pas quitter (frmNotif.Result est false), annuler la fermeture
			if (frmNotif.Result) return;
			e.Cancel = true;

			gameManager.Resume(); // Reprendre le jeu si la fermeture est annulée
		}

		// Gestionnaire du changement du nombre de clés
		private void pnlKeys_TabIndexChanged(object sender, EventArgs e)
		{
			// Met à jour les images des clés en fonction du nombre de clés possédées
			(picKey1.Image, picKey2.Image) = pnlKeys.TabIndex switch
			{
				1 => (Key, Empty),   // Une clé
				2 => (Key, Key),      // Deux clés
				_ => (Empty, Empty)    // Aucune clé (cas par défaut)
			};
		}

		// Gestionnaire du changement du nombre de points de vie
		private void pnlHealth_TabIndexChanged(object sender, EventArgs e)
		{
			// Met à jour l'image de la vie en fonction du nombre de points de vie restants
			picHealth.Image = pnlHealth.TabIndex switch
			{
				2 => Properties.Resources.fulLife, // Vie pleine
				1 => Properties.Resources.oneLife,   // Une vie
				_ => Properties.Resources.noLife   // Aucune vie
			};

			// Met à jour les images des cœurs restants
			int lives = gameManager.LevelManager.Health.Lives;
			imgLives1.Image = lives > 0 ? Properties.Resources.redHeart : Properties.Resources.blackHeart;
			imgLives2.Image = lives > 1 ? Properties.Resources.redHeart : Properties.Resources.blackHeart;
			imgLives3.Image = lives > 2 ? Properties.Resources.redHeart : Properties.Resources.blackHeart;
		}

		// Gestionnaire du changement de bonus
		private void PnlBonuses_TabIndexChanged(object sender, EventArgs e)
		{
			// Met à jour les images des bonus en fonction des bonus actifs
			(PicBonus1.Image, PicBonus2.Image) = PnlBonuses.TabIndex switch
			{
				0 => (Empty, Empty),       // Aucun bonus
				10 => (HealthKit, Empty),  // Kit de santé
				20 => (Torch, Empty),      // Torche
				12 => (HealthKit, Torch), // Kit de santé et torche
				21 => (Torch, HealthKit), // Torche et kit de santé (ordre inversé)
				_ => (Empty, Empty)       // Cas par défaut : aucun bonus
			};
		}

	}
}
Pr�sentation
============
Merci d'avoir choisi Visual Actions ! Nous esp�rons que vous l'appr�cierez autant que nous l'avons �crit.

Visual Actions vous permet d'activer une action (fonction) dans n'importe quel objet et lui passer des param�tres lors d'�v�nements s�lectionn�s par l'utilisateur (Tap, Click , Clavier, Collision ) sans avoir � �crire de code . Il est tr�s facile � utiliser. Il va tellement augmenter votre productivit� que vous vous demanderez comment vous en passer !

Pourquoi devriez-vous avoir � �crire du code juste pour "appeler" une action qui existe d�j� � l'int�rieur d'un objet (rotate ou setvelocity par exemple ?). Gr�ce � la puissance de Reflection, Visual Actions vous permet de simplement s�lectionner l'objet, choisir la fonction que vous souhaitez appeler et passer les param�tres. Le tout en mode �dition, sans toucher au code ...

Maintenant vous pouvez faire des boutons, ouvrir des portes automatiquement , des power-ups, ou appeler des routines d'initialisation , sans avoir � port�e de main de votre projet son aide en ligne.

Des tutoriaux sur Visual Actions sont disponible sur : www.vimeo.com/RaedEntertainment


Param�tres de base
==================
Who Performs Action : Cette fonction permet d�roulante vous permet de choisir le type de cible . Vous pouvez cibler l'objet appelant itsself (auto ) , un autre objet de jeu ( GameObject ) , ou d'autres classes.

Target Object : cette option appara�t uniquement si vous avez choisi " GameObject " auparavant. C'est le Game Object que vous souhaitez manipuler, l'objet qui contient l'action que vous souhaitez appeler .

Component: Le composant de l'objet cible que vous souhaitez appeler .

Action: L'action que vous souhaitez activer

Notez qu'une fois que vous choisissez un objet cible, "Component" et "Action" appara�tront.
Une fois qu'une action est choisie, les �param�tres� qui peuvent �tre transmis � cette action apparaissent comme des GUI appropri�s. Par exemple, si le param�tre est une couleur, alors un color-picker appara�tra.

When to perform Action : d�finit le moment o� l'action doit �tre d�clench�e. Notez que vous pouvez choisir plus d'une option � la fois. Ceci est utile, par exemple, lorsque vous activez � la fois "mouseClick" et "Tap", la m�me action sera activ�e � la fois sur un Tap et un clic de souris.

Notez �galement que les actions sont d�clench�es sur l'objet sur lequel le script est attach�, pas l'objet cible. Un "MouseClick " ne d�clenchera l'action que lorsque la souris sera sur l'objet pour lequel le script est attach�. Une fois d�clench�e, l'action de la composante de l'objet cible choisie sera appel�e.


Evenements
==========
At Start : D�clenche l'action d�s le chargement du script

On Update:	Se d�clenche � chaque image (frame)

On Fixed Update: D�clencher l'action apr�s un intervalle de temps fixe. Mettre le code li� � rigid-body ici, plut�t que dans "On Update".

On Mouse Click : D�clenche l'action lorsqu'un click de souris est effectu� sur le m�me objet .

On Mouse Hover : Est activ�e (� plusieurs reprises) lorsque la souris est sur l'objet.

On Tap : Se d�clenche quand un Tap du doigt est d�tect�.

On Collision : Se d�clenche d�s que cet objet entre en collision avec quelque chose d'autre.

On Collision With Character : Se d�clenche d�s que cet objet entre en collision avec le contr�leur de personnage.

On Trigger : Se d�clenche d�s que cet objet entre en collision avec quelque chose d'autre, mais est configur� pour �tre un �l�ment d�clencheur.

On Key Press : Se d�clenche d�s qu'une touche du clavier est press�e. Ceci inclut tout ce qui est support� par Unity (via KeyCode ), y compris des lettres et des touches sp�ciales comme "gauche", "droite" ou m�me "espace", "F1" ou "echap.", etc


Conseils
========
- En tant qu'utilisateur enregistr�, you are eligible to frequently recieve updates: Very early (beta) versions of the software, even before they appear on the AssetStore. To make sure you always receive the latest patch/upgrade, register yourself with us by sending us an e-mail (provided below) by e-mailing us your invoice number.

- Un objet peut �tre sa propre Target. Selectinnez simplement "self" dans la list des "who performs the action".

- Un seul objet peut appeler plusieurs objets. Il suffit d'attacher le script plusieurs fois, et assigner un objet diff�rent pour chaque instance du script (component).


============================
Ver 1.2:
-------
Ajout : Plus de d'options Target : Application, Object, The GameObject Class, Self
Ajout : Nouveau parametre d'editon : Rect
Ajout : Nouveau parametre d'editon : AnimationCurve
Ajout : "Tous" les types UnityEngine.object sont support�s via drag-and-drop: Camera, AudioClip, AnimationClip, Animation etc
Ajout : OnUpdate
Ajout : OnFixedUpdate
Ajout : Nouvel evenement : Boutons de souris Multiples (Gauche, droit, etc)*
Ajout : Nouvel evenement : OnMouseWheelMove*
Ajout : Nouvel evenement : OnAccelerometerChange* 
Ajout : Plus de scenes (tutoriaux)

(* : merci � Neoxeo pour ces ajouts)

A faire : Frame-rate independance

Ver 1.1 :
-------

Ajout : Entr�e du clavier
Ajout : Nouvelle sc�ne d'exemple pour l'entr�e au clavier
Ajout : Unity 3.5 pour une compatibilit� arri�re

Correction : correspondance ambigu� dans la m�thode de r�solution
Correction : probl�me avec le "Tap"

Ver 1.0:
-------
- Version initiale


TODO:

-Am�liorer la documentation (PDF avec des sch�mas)
-Support du cha�nage
-Visuel Actions "Manager"



Contact :
RaedContact@gmail.com
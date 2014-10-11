Présentation
============
Merci d'avoir choisi Visual Actions ! Nous espérons que vous l'apprécierez autant que nous l'avons écrit.

Visual Actions vous permet d'activer une action (fonction) dans n'importe quel objet et lui passer des paramètres lors d'événements sélectionnés par l'utilisateur (Tap, Click , Clavier, Collision ) sans avoir à écrire de code . Il est très facile à utiliser. Il va tellement augmenter votre productivité que vous vous demanderez comment vous en passer !

Pourquoi devriez-vous avoir à écrire du code juste pour "appeler" une action qui existe déjà à l'intérieur d'un objet (rotate ou setvelocity par exemple ?). Grâce à la puissance de Reflection, Visual Actions vous permet de simplement sélectionner l'objet, choisir la fonction que vous souhaitez appeler et passer les paramètres. Le tout en mode édition, sans toucher au code ...

Maintenant vous pouvez faire des boutons, ouvrir des portes automatiquement , des power-ups, ou appeler des routines d'initialisation , sans avoir à portée de main de votre projet son aide en ligne.

Des tutoriaux sur Visual Actions sont disponible sur : www.vimeo.com/RaedEntertainment


Paramètres de base
==================
Who Performs Action : Cette fonction permet déroulante vous permet de choisir le type de cible . Vous pouvez cibler l'objet appelant itsself (auto ) , un autre objet de jeu ( GameObject ) , ou d'autres classes.

Target Object : cette option apparaît uniquement si vous avez choisi " GameObject " auparavant. C'est le Game Object que vous souhaitez manipuler, l'objet qui contient l'action que vous souhaitez appeler .

Component: Le composant de l'objet cible que vous souhaitez appeler .

Action: L'action que vous souhaitez activer

Notez qu'une fois que vous choisissez un objet cible, "Component" et "Action" apparaîtront.
Une fois qu'une action est choisie, les «paramètres» qui peuvent être transmis à cette action apparaissent comme des GUI appropriés. Par exemple, si le paramètre est une couleur, alors un color-picker apparaîtra.

When to perform Action : définit le moment où l'action doit être déclenchée. Notez que vous pouvez choisir plus d'une option à la fois. Ceci est utile, par exemple, lorsque vous activez à la fois "mouseClick" et "Tap", la même action sera activée à la fois sur un Tap et un clic de souris.

Notez également que les actions sont déclenchées sur l'objet sur lequel le script est attaché, pas l'objet cible. Un "MouseClick " ne déclenchera l'action que lorsque la souris sera sur l'objet pour lequel le script est attaché. Une fois déclenchée, l'action de la composante de l'objet cible choisie sera appelée.


Evenements
==========
At Start : Déclenche l'action dès le chargement du script

On Update:	Se déclenche à chaque image (frame)

On Fixed Update: Déclencher l'action après un intervalle de temps fixe. Mettre le code lié à rigid-body ici, plutôt que dans "On Update".

On Mouse Click : Déclenche l'action lorsqu'un click de souris est effectué sur le même objet .

On Mouse Hover : Est activée (à plusieurs reprises) lorsque la souris est sur l'objet.

On Tap : Se déclenche quand un Tap du doigt est détecté.

On Collision : Se déclenche dès que cet objet entre en collision avec quelque chose d'autre.

On Collision With Character : Se déclenche dès que cet objet entre en collision avec le contrôleur de personnage.

On Trigger : Se déclenche dès que cet objet entre en collision avec quelque chose d'autre, mais est configuré pour être un élément déclencheur.

On Key Press : Se déclenche dès qu'une touche du clavier est pressée. Ceci inclut tout ce qui est supporté par Unity (via KeyCode ), y compris des lettres et des touches spéciales comme "gauche", "droite" ou même "espace", "F1" ou "echap.", etc


Conseils
========
- En tant qu'utilisateur enregistré, you are eligible to frequently recieve updates: Very early (beta) versions of the software, even before they appear on the AssetStore. To make sure you always receive the latest patch/upgrade, register yourself with us by sending us an e-mail (provided below) by e-mailing us your invoice number.

- Un objet peut être sa propre Target. Selectinnez simplement "self" dans la list des "who performs the action".

- Un seul objet peut appeler plusieurs objets. Il suffit d'attacher le script plusieurs fois, et assigner un objet différent pour chaque instance du script (component).


============================
Ver 1.2:
-------
Ajout : Plus de d'options Target : Application, Object, The GameObject Class, Self
Ajout : Nouveau parametre d'editon : Rect
Ajout : Nouveau parametre d'editon : AnimationCurve
Ajout : "Tous" les types UnityEngine.object sont supportés via drag-and-drop: Camera, AudioClip, AnimationClip, Animation etc
Ajout : OnUpdate
Ajout : OnFixedUpdate
Ajout : Nouvel evenement : Boutons de souris Multiples (Gauche, droit, etc)*
Ajout : Nouvel evenement : OnMouseWheelMove*
Ajout : Nouvel evenement : OnAccelerometerChange* 
Ajout : Plus de scenes (tutoriaux)

(* : merci à Neoxeo pour ces ajouts)

A faire : Frame-rate independance

Ver 1.1 :
-------

Ajout : Entrée du clavier
Ajout : Nouvelle scène d'exemple pour l'entrée au clavier
Ajout : Unity 3.5 pour une compatibilité arrière

Correction : correspondance ambiguë dans la méthode de résolution
Correction : problème avec le "Tap"

Ver 1.0:
-------
- Version initiale


TODO:

-Améliorer la documentation (PDF avec des schémas)
-Support du chaînage
-Visuel Actions "Manager"



Contact :
RaedContact@gmail.com
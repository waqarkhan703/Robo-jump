
Introduction
============
Thank you for choosing Visual Actions! We hope you enjoy it as much as we did writing it.

Visual Actions allows you to activate an action (function) within any object and pass parameters to it during user-selected events (Tap, Click, Keyboard, Collision) without having to write code. It is very easy to use. It will boost your productivity so much that you will wonder how you managed without it!

Why should you have to write code just to "call" an action that already exists inside an object (rotate, or setvelocity for example?). Using the power of Reflection, VisualActions allows you to simply select the object, choose which function you would like to call, and pass in the parameters, all in edit mode, without touching code…

Now you can make buttons, auto-opening doors, power-ups, or call initialisation routines, without having to litter your project with one-liner scripts.

You can view tutorials associated with Visual Actions at: www.vimeo.com/RaedEntertainment

Basic Parameters
================

"Who Performs Action":	This drop-down allows you to choose the type of target. You can target the calling object itsself (self), another game object (GameObject), or other classes.

Target Object: This only appears if you chose "GameObject" earlier. This is the Game Object which you want to manipulate. i.e, this is the object that contains the action that you would like to call.

Component: The component in the target object that you would like to call.

Action: The action that you would like to activate

Note that once you choose a Target object, "Component" and "action" will appear.
Once an action is chosen, the "parameters" that can be passed to this action appear as appropriate GUI items. For example, if the parameter is a colour, then a color-picker appears.

When to perform Action: This defines the moment at which the action is mean to be triggered. Note you may choose more than one option at a time. This is useful, for example, when you active both "Mouse click" and "tap" so that the same action will be activated on both a tap and a mouse click.

Also note that the actions are triggered based on the object to which the script is attached, NOT the target object. i.e, a "mouseClick" will trigger when the mouse is clicked on the object to which the script is attached. Once triggered, the action in the component of the chosen Target Object will be called.

Events
======

At Start:  Trigger the action as soon as the script loads

On Update:	Trigger this action every frame

On Fixed Update: Trigger this action after a fixed time interval. Put rigid-body related code here, rather than "On Update".

On Mouse Click: Trigger when the mouse has been pressed and unpressed on the same object.

On Mouse Hover: Is activated (repeatedly) when the mouse is over the object.

On Tap : Trigger when a finger tap is detected.

On Collision: Trigger as soon as this object collides with anything else.

On Collision With Character: Trigger as soon as this object collides with the character controller. 

On Trigger: Trigger as soon as this object collides with anything else, but is set to be a trigger instead.

On Key Press: Trigger on a keyboard key-press. This includes anything supported by unity (via KeyCode), including letters and special keys such as "left", "right" or even "space" or "F1" etc.

Tips
====

-As a registered user, you are eligible to frequently recieve updates: Very early (beta) versions of the software, even before they appear on the AssetStore. To make sure you always receive the latest patch/upgrade, register yourself with us by sending us an e-mail (provided below) by e-mailing us your invoice number.
-An object can be its own target. Simply select "self" from "who performs the action" drop-down.

-A single object may trigger multiple objects. Simply create more actions by clicking the "plus" sign, and assign a different object to each action. 


======================================
Ver 1.3:
-------

Added: Beta: Code View: Generates code snippets in real-time so you can copy-paste them anywhere into your own scripts, or learn from them! (in C#)
Added: Action Sequences: Add multiple actions within one component, and re-order them
Added: Re-useable Events: Now add just the events you need, and re-use them on other scripts. Code generated on the fly when needed!
Added: New Event window that allows selecting events froma tree-view. Support for "all" built-in Unity Events
Added: Call "any" Unity class (including Time, Application, GameObject, Object, Animation, Audio etc). All methods & parameters show automatically
Added: Call "any" 'C#', or 'JavaScript' Class functions, just by typing in their name. All methods & parameters show automatically
Added: Overloaded methods now supported
Added: Methods now displayed with parameter names
Added: New parameter editor: Space (World, local)
Added: Error messages: On an error, VA identifies the GameObject that caused the error
Added: Tool-tips on buttons everywhere for a more user-friendly experience

New: The chosen action is not reset when changing object/component selection if the same action can be found in the new selection
New: Methods can now be changed/added to scripts "after" selecting them from Visual Actions, without breaking.
New: Re-factored code completely.

FIXED: Now differentiates between  "Object" (UnityEngine.Object) and Component as parameter. Hence, methods such as "Instantiate" are now valid.

Ver 1.2:
-------
Added: More Target Options: Application, Object, The GameObject Class, Self
Added: New parameter editor: Rect
Added: New parameter editor: AnimationCurve
Added: Now "all" UnityEngine.object types are supported via drag-and-drop: Camera, AudioClip, AnimationClip, Animation etc
Added: New event: OnUpdate
Added: New event: OnFixedUpdate
Added: New event: Multiple mouse buttons (Left, right, etc)*
Added: New event: OnMouseWheelMove*
Added: New event: OnAccelerometerChange* 
Added: More tutorial scenes

(* Code kindly donated by neoxeo)

TODO: Frame-rate independence


Ver 1.1:
-------

Added: Keyboard Input
Added: New example scene for Keyboard Input
Added: Unity 3.5 support for people who have not upgraded to Unity 4

Fixed: Ambiguous matching in method resolution
Fixed: Problem with touch


Contact:
RaedContact@gmail.com


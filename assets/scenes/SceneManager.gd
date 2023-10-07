extends Node

@export var main_menu_scene:PackedScene
@export var universe_scene:PackedScene
@export var planet_scene:PackedScene

# Beim Hinzufügen einer Scene 
# unbedingt auch ins Array unten hinzufügen!!

var scenes:Dictionary = {
	"main_menu_scene":main_menu_scene,
	"universe_scene":universe_scene,
	"planet_scene":planet_scene
	#"":,
}

		
# Called when the node enters the scene tree for the first time.
func _ready():
	print("Entering SceneManager")
		
	var _scenes:Dictionary = {
		"main_menu_scene":main_menu_scene,
		"universe_scene":universe_scene,
		"planet_scene":planet_scene
		#"":,
		}
	scenes = _scenes
	
	for s in scenes:
		if(scenes[s] == null):
			printerr("ERROR, Scene ist null, Pfad üperprüfen!: " + s)
			OS.alert("ERROR, Scene ist null, Pfad üperprüfen!: " + s,"Scene null alert")
			get_tree().quit()
			

func get_scene(scene_name):
	print("Return scene " + scene_name)
	if (scenes[scene_name] == null):
		return null
	else:
		return scenes[scene_name]

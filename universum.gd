extends Node3D


# Called when the node enters the scene tree for the first time.
func _ready():
	print("universum start..")
	var planet_scene = load("res://Star.tscn")
	var planet = planet_scene.instantiate()
	planet.position.x = 2
	add_child(planet)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

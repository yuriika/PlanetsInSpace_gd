shader_type sky;

uniform vec3 bg_color: source_color;
uniform sampler2D skyTexture;

float rand(vec2 st) {
    return fract(sin(dot(st.xy, vec2(12.9898,78.233))) * 43758.5453123);
}
void sky() {
	float size = 100.0;
	float prob = 0.999;
	vec2 pos = floor(1.0 / size * FRAGCOORD.xy);
	float color = 0.0;
	float starValue = rand(pos);

	//if (starValue > prob)
	//{
		////vec2 center = size * pos + vec2(size, size) * 0.5;
		////float t = 0.9 + 0.2 * sin(TIME * 8.0 + (starValue - prob) / (1.0 - prob) * 45.0);
		////color = 1.0 - distance(FRAGCOORD.xy, center) / (0.5 * size);
		////color = color * t / (abs(FRAGCOORD.y - center.y)) * t / (abs(FRAGCOORD.x - center.x));
	//}
	//else if (rand(SCREEN_UV.xy / 20.0) > 0.996)
	//{
		//float r = rand(SCREEN_UV.xy);
		//color = r * (0.85 * sin(TIME * (r * 5.0) + 720.0 * r) + 0.95);
	//}
	
	if (rand(SCREEN_UV.xy / 20.0) > 0.996)
	{
		float r = rand(SCREEN_UV.xy);
		color = r * (0.25 * sin(TIME * (r * 0.8) + 720.0 * r) + 0.2);
	}
	
	//var img="";
	//get_parent().get_parent().get_node("Envioronment")
	
	vec3 color3 = bg_color + vec3(color);
	//COLOR = texture(skyTexture, SKY_COORDS).rgb + color3;
	COLOR = color3;
}

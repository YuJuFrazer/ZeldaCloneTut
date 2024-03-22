shader_type canvas_item;

uniform vec4 color_old;
uniform vec4 color_new;
uniform float tolerance = 0.1;

void fragment() {
    vec4 pixel = texture(TEXTURE, UV);
    if (distance(pixel.rgb, color_old.rgb) < tolerance) {
        pixel.rgb = color_new.rgb;
    }
    COLOR = pixel;
}
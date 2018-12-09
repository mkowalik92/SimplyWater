This compute shader generates a noise texture(grayscale). Currently I use a xor shift, originally outlined in George Marsaglia's paper(page 4), to generate a random float value between 0 and 1. I then store this float value in the rgb values of a 2D texture. This texture is the generated noise texture. A seed is based on a user inputed uint(seed) multiplied by the threadID for more variation. This can just be replaced with just the threadID with not inputed seed from the user.

This compute shader is the base for tessendorf's method of simulating ocean waves. I will either use a series of pregenerated noise textures that this shader generates and then use a texture lookup to get random numbers for the gaussian numbers required in the wave height field calculations. 

Also, there may be a slight problem with the noise this compute shader generates. It sometimes gives a distinct black border on 1-2 edges of the textures. I will look into that...

##### ++RUNNING++
- Create an empty game object in unity and attached the c# script to the game object.
- Create a quad/plane/mesh with a material using the standard shader.
- Drag and drop the material onto the material slot in the gameobject/script in the unity editor to link them together.
- Play the game and the compute shader/script will automatically update the albedo texture of the material using the generated render texture.
- The script updates every second, but can be easily adjusted to update whenever you want. Currently I am experiencing no fps lag when running the shader even with very render texture output resolution.

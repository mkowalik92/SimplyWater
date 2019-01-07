# SimplyWater
###### This doc contains my blog/record/notes/thoughts about this open source project. This will be eventually moved to a more "professional looking" development blog located on my personal website, whenever I actually feel like sitting down and finishing it up... Having too much fun with shaders and opengl lately.
---
+ 1/6/2019 - Hit a little road bump after implementing the butterfly compute shader. I have to move the rendering of the displacement textures/shader calls to a low level native plugin.
+ 12/28 - Making great progress on the ocean simulation. The h0k compute shader works flawlesly and I am getting identical results for the h0k and h0minusk textures.
+ 12/9 -  Currently finalzing a shader that creates noise textures(small problem that can be addressed later). I will be working on creating the compute shaders for calculating (tilde h0(k)) and (tilde minus h0(k)), then the shader to combine both tilde h0(k)s into the time dependent h0(kt). Then I will mess around with texture sampling and noise generation, run some tests, and then pick the mose optimized method. 
+ 12/4 - Doing some more shader practice to hone my skills and learn about compute shaders. Going to be using compute shaders for the height map used in simulating water using tessendorf's method. The caustic projector is being put on hold because I think I have to create my own simple projector instead of using unity's built-in one. Will most likely be starting over, again, from scratch. 
+ 11/06 - Need to create a custom unity projector shader. The default multiply shader is giving me weird visual artifacts, I think...
+ 9/11 - Big changes coming soon.(Guess not...)
+ 8/14 - Started from scratch again. Began by implementing simple skybox reflection on the top water plane. Made a decent depth shader and added transparency. Now I need to combine those effects to make something decent. Started working on refraction and will begin fresnel/edge foam soon since the depth texture is taken care of. 

### --- IN PROGRESS ---
+ Vertex Displacement Map
    + ~~h0(k)~~ + ~~h0(-k)~~ -> ~~twiddle factors~~ -> ~~h(k,t)~~ -> fft(cooley-tukey/butterfly algorithm) -> inversion(obtain spatial domain)

### --- TO DO ---

##### top_water
+ ~~Reflection~~
+ Refraction
+ ~~Transparency~~
+ Fresnel
+ Edge Foam
    + Shore
    + Object
+ ~~Depth~~
+ Underwater caustics
+ Vertex Displacement - Create wave height map - FFT Simulation based on nvwaveworks/tessendorf
+ Puddles
+ Sound Effects
+ Physics/Bouyancy
+ Ocean Spray Particles
+ Foam Particles(Motion)

### --- Notes ---
Experimenting with caustics. Projecting the texture from above the water/through it is probably the best solution? Projector follows the camera and intensity/distortion from noise changes depending on players distance from floor and whether the caustics are being viewed from above water/below. Maybe generate a height map depending on wave peak and feed that height map(stress field) to the projector shader and this will generate(possibly?) the caustics in the right locations on the object below the water. Then I wont need to use a caustic texture and can instead use(maybe) a modified algorithm for surface crack patterns to generate the caustics.

The vertex displacement will be handled by calculating the wave height field using tessendorf's method. I will compute the wave height field in the frequency domain and then convert the heights to the spatial domain with the Cooley-Tukey FFT algorithm. This will require me to calculate the twiddle indices for the butterfly transform. I will need compute shaders for calculating h0k and -h0k, initially, at the same time. Then combining (tilde h0(k)) and (tilde minus h0(k)) into the time dependent h0(kt) every frame. After the combination I will use the twiddle indices(generated texture) to convert the h0(kt) into the spatial domain and then output these results into a displacement texture containing float values. This displacement texture needs to be generated each frame. The (tilde h0(k)) and (tilde minus h0(k)) need to be only generated once since they are not time dependent. Pretty sure the same thing goes with the twiddle indices texture. Currently I have finished the random number generator and the gaussian number generator, using the box-mueller transform method and xor shifts. I can look into possibly using noise textures sample numbers to generate the random numbers required for the box-mueller transform. My current method of xor shifts may be a bit too compute heavy since this will be required to run every frame. Not sure whether sampling texture data every frame will optimize my code or slow it down. Currently I can generate noise texture very easily with the xor shift algorithm I am using now, but it is giving me weird results along the border pixels only resulting in a black line of pixels. This may just be coincidence or I am messing up somewhere. The x-z displacement, choppy waves, does not need to be implemented immediatly, but it will help if I incorporate it from the start. The plan is to document this very well so people in the future wanting to implement tessendorf's paper will be able to do it with ease, with a tutorial, instead of using multiple different, scattered, sources. After implementing tessendorf's method I will look into using nvidia's implementation of tessendorf's, which uses multiple cascading fft simulation to generate their water that they use in waveworks.

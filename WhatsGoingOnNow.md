# SimplyWater
-------------
+ 12/4 - Doing some more shader practice to hone my skills and learn about compute shaders. Going to be using compute shaders for the height map used in simulating water using tessendorf's method. The caustic projector is being put on hold because I think I have to create my own simple projector instead of using unity's built-in one. Will most likely be starting over, again, from scratch. 
+ 11/06 - Need to create a custom unity projector shader. The default multiply shader is giving me weird visual artifacts, I think...
+ 9/11 - Big changes coming soon.(Guess not...)
+ 8/14 - Started from scratch again. Began by implementing simple skybox reflection on the top water plane. Made a decent depth shader and added transparency. Now I need to combine those effects to make something decent. Started working on refraction and will begin fresnel/edge foam soon since the depth texture is taken care of. 

### --- IN PROGRESS ---
+ Learning about compute shaders.
+ Honing my skills.
+ Caustics
+ Vertex Displacement

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

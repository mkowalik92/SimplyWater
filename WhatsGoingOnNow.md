# SimplyWater
-------------
+ 11/06 - Need to create a custom unity projector shader. The default multiply shader is giving me weird visual artifacts, I think...
+ 9/11 - Big changes coming soon.
+ 8/14 - Started from scratch again. Began by implementing simple skybox reflection on the top water plane. Made a decent depth shader and added transparency. Now I need to combine those effects to make something decent. Started working on refraction and will begin fresnel/edge foam soon since the depth texture is taken care of. 

### --- IN PROGRESS ---
+ Edge Foam
+ Refraction
+ Fresnel
+ Caustics

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
+ Vertex Displacement
+ Puddles
+ Sound Effects
+ Physics/Bouyancy
+ Ocean Spray Particles
+ Foam Particles(Motion)

### --- Notes ---
Under water caustics should be used as some kind of distortion texture(animated through uv displacement) that is applied to a refraction shader for the bottom water plane.

Experimenting with caustics. Projecting the texture from above the water/through it probably the best solution? Projector follows the camera and intensity/distortion from noise changes depending on players distance from floor and whether the caustics are being viewed from above water/below. 11/06

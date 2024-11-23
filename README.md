1 – Main Terrain Implementation: 
For the implementation you will need to incorporate features such as: 
• Terrain generation: Use methodologies such as cellular, arbitrary shapes or noise (or 
hybrid) to generate your terrain. For the noise you can use Perlin. 
• Terrain generation advanced: Implemented with Perlin Noise or through your own 
code solution, additional layers of procedural generation. 
O	 Layering Multiple Noise Functions (e.g., Simplex noise or Worley noise) 
o 	Fractal Noise (Octave Noise) 
o 	Voronoi-Based Terrain Segmentation 
o 	Height-Based Terrain Texturing [Week 2 – theory] 
 
• Generating content:   
a. Based on your choice of environment/terrain, set distinctive rules (procedures) 
for generating objects and incorporating them into the environment – Use a set 
of 6 unique objects [e.g. Health potions, coins, weapons, magic pick-up, shield, 
trap, loot etc.]  
 
Important: You need to generate, in total, 6 types of objects, multiple times 
each. You will need at least 3 of each object in the environment. 

b. Advanced Object Generation: Use the built-in NavMesh pathfinder package 
and tools to verify that each of these objects are accessible to the player (e.g. 
not blocked behind water that they cannot cross) during generation, prior to 
being placed on the map.  
Add a visualisation for successful generation of each object, reaching between 
the player and object location - Pathfinder visualisation should be displayed as 
an option that can be enabled/disabled. 

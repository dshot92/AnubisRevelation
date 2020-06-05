# LowPolyVGD

\-     **Obbiettivi del giuoco**

L’obbiettivo principale del gioco consiste nel superare i diversi livelli presenti superando le varie sfide che verranno poste al giocatore (sconfiggere i nemici e raccogliere il numero di gemme richieste) arrivando così alla battaglia con il boss finale e al salvataggio della principessa.



\-     **Principali dettagli tecnici**

Il gioco consisterà in un Platform 3D. Avrà a disposizione esclusivamente la modalità giocatore singolo. Le armi utilizzate saranno armi medievali o comunque NON da fuoco quindi non verrà implementato nessuna simulazione di proiettili. Per quanto riguarda la presenza di nemici, collezionabili e altro prenderemo come riferimento le linee guida presenti nelle istruzioni fornite a lezione.



\-     **Assets**

Per l’utilizzo di assets, ci baseremo sull’asset store di Unity ed eventualmente ad altri siti in modo. Se necessario provvederemo a modificare qualche aspetto in base alle nostre necessità.



\-     **Scene e livelli**

Il gioco sarà caratterizzato da un’unica scena (open world) con tre diverse ambientazioni, ognuna delle quali corrisponderà a un livello differente. Per poter passare da un livello all’altro sarà necessario uccidere tutti i nemici e talvolta recuperare determinate gemme. Il passaggio da un livello all’altro avverrà tramite portoni o archi o ponti.  Nell’ultima ambientazione nonché ultimo livello sarà presente anche il super boss finale che sarà molto più difficile da sconfiggere. I livelli non potranno essere superati se non si raggiungono gli obbiettivi. L’idea alla base è quella di utilizzare pedane a pressione o teletrasporto, entrambi sbloccabili solo quando gli obiettivi del livello descritti sopra sono stati raggiunti. In ogni livello ci saranno mostri con caratteristiche diverse, nell’ultimo livello invece sarà il boss a generare mostri di supporto ad intervalli regolari, rendendo la sfida più avvincente.  



\-     **Gameplay**

Una volta avviato il gioco, il giocatore potrà scegliere quale livello giocare e sarà presente anche un breve tutorial in cui gli verranno mostrati, attraverso delle finestre di dialogo, tutti i comandi con la possibilità di effettuare una prova.

Il gioco potrà essere salvato in ogni momento mediante il menù di gioco, per consentire al giocatore di riprendere dal punto in cui è arrivato in qualsiasi momento.



In ognuno dei livelli saranno disponibili i seguenti **collezionabili**:

1.  Un cuore nascosto (o dropabile dai nemici) che conferirà una vita aggiuntiva al personaggio principale (in presenza di slot liberi) altrimenti verranno attribuiti punti extra;

2.  Una o più armi sparse per le varie ambientazioni, come ad esempio spade, asce, ecc…. In ogni caso come specificato ci limiteremo ad armi fisiche. Le armi potranno essere scambiate con l’arma attuale. Ogni arma avrà un suo danno associato;

3. Gemme che oltre a far aumentare il punteggio, in alcuni casi saranno strettamente necessarie per progredire all’interno del gioco.



Nel passaggio tra i livelli sarà inserito un piccolo enigma facilmente risolvibile (es. spostare un blocco di pietra sopra una determinata zona per aprire la porta (pedana a pressione), rompere un lucchetto, trovare una chiave, ecc…).

I nemici potranno essere sconfitti tramite l’arma del giocatore, allo stesso modo i nemici infliggeranno danni al giocatore tramite le armi o le abilità in loro possesso. Andando avanti con le ambientazioni, la difficoltà aumenterà in maniera progressiva.

All’interno del gioco ci saranno a disposizione 3 differenti **power-up** per far fronte alle difficoltà:

1. 40 secondi di danni extra, abilità che si sbloccherà dopo aver raccolto il relativo segnalino, idealmente a forma di spada;

2.  40 secondi di armatura, che forniranno al giocatore una sorta di aura protettiva che lo proteggerà dai danni per tutta la durata (protezione totale) che si sbloccherà dopo aver raccolto il relativo segnalino, idealmente a forma di scudo;

3. 40 secondi di velocità extra, che permetteranno di muoversi più rapidamente in modo da far fronte a più nemici contemporaneamente o se necessario a cercare un riparo in modo da elaborare una strategia differente in base alla situazione in cui ci si trova. Il segnalino questa volta ricorda una clessidra.

   ![image-20200509174712993](README.assets/image-20200509174712993.png)

------


### File Sources
Done in Blender:
-Amulet.fbx
-Flash.fbx
-pyramid.fbx
-sphinx.fbx
-biceps.fbx
-desert6.fbx
-sarcofagus.fbx
-coin.fbx
-vase.fbx
-logo_3d.fbx

LowPoly Environment Pack
https://assetstore.unity.com/packages/3d/environments/landscapes/lowpoly-environment-pack-99479

Standard Assets Unity
https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2017-3-32351

Low Poly Warrior (Player)
https://sketchfab.com/3d-models/simple-low-poly-warrior-dfa80ceee90c4ec49d3d785c22304ee8

PolyBrush (From package manager)
https://unity3d.com/unity/features/worldbuilding/polybrush

Low-Poly Environment Assets
https://sketchfab.com/3d-models/low-poly-environment-assets-2d7f5f53dfe24f419cc0ac4dc4dd17a1

Low Poly Coin
https://sketchfab.com/3d-models/lowpoly-gold-coin-34794c00e9d140f6b86e930fef18b009

Low Poly Mummy
https://assetstore.unity.com/packages/3d/characters/free-mummy-monster-134212

Polygonal Foliage Asset Package
https://sketchfab.com/3d-models/polygonal-foliage-asset-package-e3c1a1a51788440c9c26c155e6b40f8f

Simple Gems Ultimate Animated Customizable Pack
https://assetstore.unity.com/packages/3d/props/simple-gems-ultimate-animated-customizable-pack-73764

Save Game Assets
https://assetstore.unity.com/packages/tools/input-management/save-game-110382#reviews

Game Music
https://krystal-may.itch.io/into-the-sand-krystal-may

FREE Skybox Extended Shader
https://assetstore.unity.com/packages/vfx/shaders/free-skybox-extended-shader-107400

Desert Wind Loop Sound
https://retired.sounddogs.com/sound-effects/wind-barstow-gusty-moan-156366

Low Poly Rocks Pack
http://unityassetcollection.com/low-poly-rocks-pack-free-download/

Sound Effects
https://www.soundsnap.com/

Realtime NavMesh Surface
https://github.com/Unity-Technologies/NavMeshComponents

Anubis 3D models
https://sketchfab.com/3d-models/anubis-46b69c24f12446ec9250a05aa39f79f3

Anubis 3D models with clothes
https://sketchfab.com/3d-models/anubis-with-clothes-2480d344ea694f458b4646eb876b5792

Anubis Sarcofagus 3D models
https://free3d.com/3d-model/anubisegyptianjackal-v2--985424.html

Petra model
https://3dwarehouse.sketchup.com/model/adbd74a7-c1f7-4d08-8890-ddd08a3dbc08/petra?login=true#

Historic environment (For Torch)
https://assetstore.unity.com/packages/3d/environments/historic/historic-environment-142116

Unity Particle Pack
https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325?utm_source=youtube&utm_medium=social&utm_campaign=education_global_generalpromo_2018-09-04_particle-pack&utm_content=download_video

Night time Nature Sounds - High Desert Nightlife - Crickets, Relaxing
https://www.youtube.com/watch?v=r9Ii50ZiEYU

Rattle Snake Shaker Snaking A4 Sound Effect
https://www.fesliyanstudios.com/royalty-free-sound-effects-download/rattlesnake-281

Boss Fight sound
https://youtu.be/Ern_4rdhOuw


------

### TODO
- [X] Implement Damaging system
- [x] Enemy random walk?
- [x] Sandy jump sound
- [x] Mummies Sand footsteps sound (Sound Locations???)
- [x] Volume Slider (in main menu and pause menu)
- [x] Audio in game ( music(Tomb Raider??) + wind effect )
- [X] ESC pause game
- [X] Resolt first esc press problem
- [x] Loop storm sound in-game
- [x] Eye Exposure Adaptation
- [X] World space Health bar (while watching down it can be seen)
- [x] Player sound is too loud
- [x] Audio Stuff
- [x] Pause Menu won't work on Scene2 (Creating a Canvas always create a Event System -Adding one solved problem)
- [x] GameManager Singleton
- [x] Main menu camera wonders around to much. Clamp it straight
- [x] Amulet Blender
- [X] Collectables
- [X] PowerUp are seconds dependant (coroutines)
- [x] Resolve Audio Bug on Level1 Load
- [x] Spawn Point in level eye should showcase Eye exposure adaptation.
- [X] Glow Texture with promt to jumo with space on the first ledge?
- [x] FPS Counter
- [x] Power Up sound
- [x] Movement, attack, climb (Prompt)
- [X] It's a fucking Desert. Can I Dig? it works with Mesh Filter
- [X] How to ReTriangulate around not to show void?? (Done by creating mesh from blender)
- [X] Levels 2 layout
- [x] Enemy Death status and animation
- [X] Night Light in level3
- [x] .Exe Game Icon
- [x] Levels 3 - Add Details
- [x] Final Boss Anubi? (Blender??)
- [x] Create Camera Post Process Profile For night effect on Level3
- [x] Snake on Level3
- [x] Lights around Level3
- [X] GUI (ex coins)
- [X] Coin Sound, logic and world placement
- [x] Add sound when attacking
- [x] Boss TP behind after some distance?
- [x] Prompt to get Sword GUI (Screen Space, not to get blurred) and level 1 start
- [x] Sand Storm Effect on menu???
- [x] Logic for boss fight (trigget box over separator to start fight)
- [x] When dying healt should not get up to full before exiting to menu
- [x] Pause menu loading - saving buttons
- [x] Boss Healt GUI

- [ ] Handle not spawning torch and sword, if player already has it
- [ ] Amulet To advance Prompt
- [ ] dead player sound - Can i slow time when is dead?
- [ ] Game Over - Win confirmations
- [ ] Transition Animaiton during Scene Loading ( To get to next Level find Amulet)

Probably not going to implement
- [ ] Load - Save (Binary Formatter!! - NO( *Player prefs have no transform data* ))
- [ ] Final build to have only 1 player and don't destroy him while scene switching??

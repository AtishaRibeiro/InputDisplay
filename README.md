# Ghost Input Display

An input display that uses Mario Kart Wii RKG files to display inputs.

Built-in record function that can create transparent videos.

## Ghost Cheat Detection

You'll be able to detect 3 types of cheating. When you selected the type(s) of cheat(s) you want to test for the ghost, click `Check` to view the Cheat Report with the results. The cheat types are:

    Rapid Fire
    Illegal Inputs
    Live Replay

### 1. Rapid Fire

`Rapid Fire` checks if the ghost performs inhumanly fast controller inputs. It only checks for the wheelie button being rapid fired as that's in general the most used rapid fire button to get guaranteed luck wheelies. You can select from a dropdownlist what size of framegap you'd like to flag as being rapid fire inputs. Pure Rapid Fire code uses 1 frame gap size, but 2 frames gaps are still way too small for humans to consistently perform, so it's there as an option. A 3 frames gap starts to be humanly possible (still incredibly fast) so it's not an option.

![Rapid Fire Image](https://camo.githubusercontent.com/ba5e274d1a3334eb114c71c8eb8e0e1151446e79/68747470733a2f2f692e6779617a6f2e636f6d2f30353637666235366666356262616631333838383732396431653162383031392e706e67)

*(Nagisa's 02:31.942 on rBC used as an example of Rapid Fire detection)*
### 2. Illegal Inputs

`Illegal Inputs` checks if all the analog inputs are possible for the controller that the run used. The Wii Wheel doesn't have input restrictions, but the GCN, Classic & Nunchuck have some sort of restrictions because of how the joystick is in a circular shape. So, for example, no full up-left input is possible for the GCN, Classic or Nunchuck, but is for the Wii Wheel.


![Illegal Inputs Image](https://camo.githubusercontent.com/cb675cdd2171d4d58a749a7f7b33f7083e8f02ca/68747470733a2f2f692e6779617a6f2e636f6d2f63353432383561656362663265313866363664636561313833636637663665652e706e67)

*OJ's 00:30.383 on MG glitch used as an example of Illegal Input detection. It's originally driven as a Wii Wheel time, but this ghost is a Live Replay ghost that happened to be uploaded to the database.)*

### 3. Live Replay

`Live Replay` checks if the ghost performs the same exact inputs as another selected ghost file. Live Replay only kicks in at the start of the run and stops whenever the player itself interupts. So it only checks detects when the first inputs from the start are the same for long enough.


![Live Replay Image](https://camo.githubusercontent.com/7f80df36fcd8aa671e366617923dce5d8b553a15/68747470733a2f2f692e6779617a6f2e636f6d2f34306234313461623062653765653436356461353365663133383630666231312e706e67)

*(Nooboss's rDH times (01:35.857 and 01:35.775) were used for the example of Live Replay detection)*

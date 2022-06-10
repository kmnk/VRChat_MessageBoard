# MessageBoard
Simple system for input and share text in VRChat World

## Contents
- README.md : this file
- README_ja.md : README.md translated into Japanese
- MessageBoard_vX_Y_Z.unitypackage
- MessageBoard.cs : main script
- LogLine.cs : script for each message line

### in unitypackage
* Kmnk
    * MessageBoard
        * Prefabs
            - MessageBoard.prefab
        * Scripts
            * Editor
                - UdonBehaviourExtensions.cs
                - UdonSharpBehaviourExtensions.cs
            - MessageBoard.cs
        * Udon
            - LogLine.cs
            - MessageBoard.cs

## Usage
1. Import UdonSharp
2. Import this MessageBoard unitypackage
3. Place MessageBoard Prefab in scene

## Features
Basically, select the MessageBoardCanvas object and set the settings in the Message Board on the Inspector

### Only World Owner Mode
Only world owner can input message. The input button will not be displayed to not world owner users.

### Initial Messages
If Initial Messages, Initial Name, and Initial Time are specified, messages are initially entered as initial values.

### Title
Specify the text string to be displayed at the top of the message board.

## Notes
- I have confirmed that this works with Unity 2019.4.31f1, VRCSDK3 WORLD 2022.06.03.00.03 Public, UdonSharp v0.20.3

## License
MIT License
Copyright (c) 2022 KMNK

## 更新履歴
- 2022/06/10 v1.1.1 Order UI
- 2022/06/04 v1.1.0 Add new features
    - initial messages
    - only world owner mode
- 2022/06/04 v1.0.0 Released
# MessageBoard
Simple system for input and share text in VRChat World

## Contents
- README.md : this file
- README_ja.md : README.md translated into Japanese
- MessageBoard_vX_Y_Z.unitypackage
- Scripts/ : C# Scripts
- Udon/ : U# Scripts
- LICENSE

### in unitypackage
* Kmnk
    * MessageBoard
        * Prefabs
            - MessageBoard.prefab : main prefab
            - InputBoard.prefab
        * Scripts
            * Editor
                * Extensions
                    - UdonBehaviourExtensions
                    - UdonSharpBehaviourExtensions
                - BuildProcessor
                - EditorBase
                - InputBoardEditor
                - MessageBoardEditor
            - InputBoard.cs
            - MessageBoard.cs
        * Udon
            - InputBoard.asset
            - InputBoard.cs
            - LogLine.asset
            - LogLine.cs
            - MessageBoard.asset
            - MessageBoard.cs
            - TemplateMessageButton.asset
            - TemplateMessageButton.cs

## Usage MessageBoard
1. Import UdonSharp
2. Import this MessageBoard unitypackage
3. Place MessageBoard Prefab in scene

### Usage InputBoard
After MessageBoard placed, Place InputBoard Prefab under in the scene.

## Features MessageBoard
### Core
#### Id
When placing multiple MessageBoards in one world, specify the Id corresponding to each prefab.
If only one MessageBoard is to be placed, leave it at 0.

### Option
#### Only World Owner Mode
Only world owner can input message. The input button will not be displayed to not world owner users.

### Title
Specify the text string to be displayed at the top of the message board.

### Initial Messages
If Initial Messages, Initial Name, and Initial Time are specified, messages are initially entered as initial values.

## Features InputBoard
### Core
#### Id
When placing multiple MessageBoards in one world, specify the Id corresponding to each prefab.
If only one MessageBoard is to be placed, leave it at 0.

### Option
#### Pickupable
Select whether you want to be able to pick up the InputBoard.

#### TemplateMessages
Specify the template words for entering text by pressing the button.

## Notes
- I have confirmed that this works with Unity 2019.4.31f1, VRCSDK3 WORLD 2022.06.03.00.03 Public, UdonSharp v0.20.3

## License
MIT License
Copyright (c) 2022 KMNK

## Updates
- 2022/06/13 v1.2.0 Add InputBoard for supporting input template words
- 2022/06/10 v1.1.1 Order UI
- 2022/06/04 v1.1.0 Add new features
    - initial messages
    - only world owner mode
- 2022/06/04 v1.0.0 Released
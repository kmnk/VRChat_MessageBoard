# MessageBoard
VRChat のワールドに設置して、キーボードで入力したテキストを共有するための簡単な仕組みです。

## 内容
- README.md : このファイルの内容を英語で記述したもの
- README_ja.md : このファイル
- MessageBoard_vX_Y_Z.unitypackage
- Scripts/ : C#スクリプト
- Udon/ : U#スクリプト
- LICENSE

### in unitypackage
* Kmnk
    * MessageBoard
        * Prefabs
            - MessageBoard.prefab : MessageBoard 本体 prefab
            - InputBoard.prefab : 入力補助ボード prefab
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

## MessageBoard 使い方
1. UdonSharp を Import
2. MessageBoard の unitypackage を Import
3. Kmnk/MessageBoard/Prefabs 下の MessageBoard Prefab をシーンに配置

### InputBoard 使い方
MessageBoard を設置後、 Kmnk/MessageBoard/Prefabs 下の InputBoard Prefab をシーンに配置

## MessageBoard 機能
### Core
#### Id
1ワールド内に複数の MessageBoard を設置する際、別 prefab に対応する Id を指定します。
1つのみ設置する場合は 0 のままで問題ありません。

### Option
#### Only World Owner Mode
ワールドオーナーのみ書き込みができるモードです。ワールドオーナー以外には入力ボタンが表示されません。

#### Title
伝言板上部に表示される文字列を指定します。

### Initial Messages
Initial Messages, Initial Name, Initial Time の指定で、ワールド生成時に初期値としてメッセージが入ります。

## InputBoard 機能
### Core
#### Id
1ワールド内に複数の MessageBoard を設置する際、対応する MessageBoard の Id を指定します。
1つのみ設置する場合は 0 のままで問題ありません。

### Option
#### Pickupable
InputBoard をピックアップできるようにするかを選択します。

#### TemplateMessages
ボタンを押してテキストを入力するテンプレート文言を指定します。

## その他
- Unity 2019.4.31f1, VRCSDK3 WORLD 2022.06.03.00.03 Public, UdonSharp v0.20.3 で動作を確認しています

## License
MIT License
Copyright (c) 2022 KMNK

## 更新履歴
- 2022/06/13 v1.2.0 テンプレート文言入力補助機能 InputBoard を追加しました
- 2022/06/10 v1.1.1 Editor 拡張を入れて UI を整理しました
- 2022/06/04 v1.1.0 機能を追加しました
    - initial messages : 初期メッセージ機能
    - only world owner mode : ワールドオーナーしか書き込めないようにするモード
- 2022/06/04 v1.0.0 公開
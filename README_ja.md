# MessageBoard
VRChat のワールドに設置して、キーボードで入力したテキストを共有するための簡単な仕組みです。

## 内容
- README.md : このファイルの内容を英語で記述したもの
- README_ja.md : このファイル
- MessageBoard_vX_Y_Z.unitypackage
- MessageBoard.cs : メインスクリプト
- LogLine.cs : 各メッセージの行を表現するスクリプト

### in unitypackage
* Kmnk
    * MessageBoard
        * Prefabs
            - MessageBoard.prefab : そのままシーンに設置すれば使える Prefab
            - MessageBoardCanvas.prefab : uGUI Canvas を持ったオブジェクトの Prefab （分かる人だけ使ってください）
        * Scripts
            - LogLine.cs
            - MessageBoard.cs

## 使い方
1. UdonSharp を Import
2. MessageBoard の unitypackage を Import
3. Kmnk/MessageBoard/Prefabs 下の MessageBoard Prefab をシーンに配置

## 機能
基本的に MessageBoardCanvas オブジェクトを選択して Inspector の MessageBoard Udon Behaviour にある項目で設定します

### Only World Owner Mode
ワールドオーナーのみ書き込みができるモードです。ワールドオーナー以外には入力ボタンが表示されません。

### Initial Messages
Initial Messages, Initial Name, Initial Time の指定で、ワールド生成時に初期値としてメッセージが入ります。

### Title
伝言板上部に表示される文字列を指定します。

## その他
- Unity 2019.4.31f1, VRCSDK3 WORLD 2022.06.03.00.03 Public, UdonSharp v0.20.3 で動作を確認しています

## License
MIT License
Copyright (c) 2022 KMNK

## 更新履歴
- 2022/06/04 v1.1.0 機能を追加しました
    - initial messages : 初期メッセージ機能
    - only world owner mode : ワールドオーナーしか書き込めないようにするモード
- 2022/06/04 v1.0.0 公開
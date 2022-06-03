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

## その他
- Unity 2019.4.31f1, VRCSDK3 WORLD 2022.06.03.00.03 Public, UdonSharp v0.20.3 で動作を確認しています

## License
MIT License
Copyright (c) 2022 KMNK

## 更新履歴
- v1.0.0 公開
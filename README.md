ゲーム再生動画URL ７月１５日　更新
--
https://www.youtube.com/watch?v=U_1SCqz6c80&ab_channel=%EB%B0%95%EC%84%9C%EC%A7%84


「ゲームタイトル 」
--
Throne of Ruins　廃墟の王座／開発期間：5月０１日～7月下旬（見込み）


「ゲームテーマ 」
--
モンスターにより廃墟となった王国の王様が主人公（剣闘士）に依頼することになる。
主人公は一応、依頼を受けて王国に向かいますが、思ったより深刻な状況を拝見し、驚きます。
その王国の再建のために必死に頑張る主人公の物語を描いたRPGゲーム。


「ゲームシーン構成」
--
１. LoginScene (ログインシーン)

目的: プレーヤーのログインとニックネームの入力を処理します。

機能:プレイヤーログインの実装、ニックネーム入力フィールド提供、ニックネーム入力後、Enterキー入力時に次のシーンに移動

２. Viliage(町)

目的: ゲームの主要なアクティビティが行われる基本的なスペースです。

構成: プレーヤーが活動できる基本的な環境を実現 (店舗、NPC、インベントリーシステムなど)、他のシーンに移動できる進入点の役割

３. Dungeon1 (Wizard)

目的: 特定のダンジョン プレイのためのシーン

機能:　ダンジョン探検と戦闘、ダンジョン目標達成(例:ボスチャーチ、経験値獲得)、	ダンジョン完了後、GameSceneに復帰可能

４. Dungeon2 (Goblin)

内容： Dungeon1と同じ

5. BossDungeon

目的:　最終プレーヤーとボースとの戦闘システム

機能:　ボスフェーズシステム

１００％～７０％　：フェーズ１　→　基本的なPatrol後攻撃範囲内にプレーヤーがいると攻撃する

７０％～　４０％　：フェーズ２　→　攻撃範囲内での攻撃は維持しながらも一定時間ごとに火炎オブジェクトを生成(プレイヤー攻撃可能)

４０％～　０％（死）：フェーズ３　→　フェーズ２でのシステム維持　＋　モンスター召喚(召喚されたモンスターは自分の攻撃範囲にプレーヤーが侵入した場合、追跡後攻撃ダンジョン1·2システムのモンスターと同じ）
   

「最終コンテンツ」
--
１．カットシーン（始めて町で王様とのやり取り、ボースダンジョン入場ボースとのやり取り）
２．全体的なシステム安定性修正、UI、サウンドなどUX的な物追加

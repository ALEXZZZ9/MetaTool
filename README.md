MetaTool For Nintendo Switch
===========================
Tools for working with npdm, desc files.

Features
=====
MetaTool
* Сonverting npdm files to desc files.
* Make npdm from desc and meta.
* Update acid in desc.

MetaToolGUI
* Сonverting npdm files to desc files.
* Desc editor.

Usage
=====
```
Usage: MetaTool.exe [options...]
Options:
NpdmToDesc <npdmFile> <outputDescFile>  - Convert npdm to desc
MakeNpdm <descFile> <metaFile> <outputNpdmFile> - Make npdm from desc and meta
UpdateAcid <descFile> optional:<metaFile> - Update acid in desc
Samples:
MetaTool.exe NpdmToDesc .\main.npdm .\main.desc
MetaTool.exe MakeNpdm .\main.desc .\main.nmeta .\main.npdm
MetaTool.exe UpdateAcid .\main.desc .\main.nmeta
```
>Or just use MetaToolGUI.

License
=======
MIT License. See attached `LICENSE.md` file.

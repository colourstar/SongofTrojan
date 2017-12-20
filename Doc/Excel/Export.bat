tabtoy.exe ^
--mode=v2 ^
--json_out=Config.json ^
--combinename=Config ^
--lan=zh_cn ^
Item.xlsx+Item_Equip.xlsx+Item_Pet.xlsx

 
copy .\Config.json ..\..\SongofTrojan\Assets\Resources\Config\
@IF %ERRORLEVEL% NEQ 0 pause
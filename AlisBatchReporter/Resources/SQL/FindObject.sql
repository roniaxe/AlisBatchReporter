SELECT
   GBA.BATCH_RUN_NUM as "Batch Run No",
   TT.TASK_NAME as "Task",
   GBA.TASK_ID as "Task ID",
   GBA.ENTRY_TIME as "Time",
   GBA.primary_key_type as "Primary Key Type",
   GBA.primary_key as "Primary Key",
   GBA.secondary_key_type as "Secondary Key Type",
   GBA.secondary_key as "Secondary Key",
   GBA.entry_type as "Entry Type",
   GBA.description as "Details"
FROM
   G_BATCH_AUDIT GBA,
   T_TASK TT 
WHERE
   1=1
   AND (GBA.PRIMARY_KEY LIKE {Param3} {Param4} {Param5})
   AND GBA.ENTRY_TIME > {Param1} 
   AND GBA.ENTRY_TIME - 1 <= {Param2} 
   AND TT.TASK_ID = GBA.TASK_ID 
ORDER BY
   PK DESC;
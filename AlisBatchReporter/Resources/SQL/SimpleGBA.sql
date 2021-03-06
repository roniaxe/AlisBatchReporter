SELECT
   GBA.DESCRIPTION,
   GBA.ENTRY_TIME,
   GBA.ENTRY_TYPE,
   GBA.BATCH_RUN_NUM,
   GBA.PRIMARY_KEY_TYPE,
   GBA.PRIMARY_KEY,
   GBA.SECONDARY_KEY_TYPE,
   GBA.SECONDARY_KEY,
   GBA.BATCH_ID,
   GBA.TASK_ID,
   GBA.SERVER_NAME
FROM
   G_BATCH_AUDIT GBA 
WHERE
   GBA.BATCH_RUN_NUM = {Param1}
AND
   GBA.TASK_ID = {Param2}
{Param3}
ORDER BY
   GBA.DESCRIPTION
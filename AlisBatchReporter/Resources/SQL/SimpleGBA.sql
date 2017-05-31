SELECT
   * 
FROM
   g_batch_audit 
WHERE
   batch_run_num = {batchRunNum}
   AND entry_type IN 
   (
      5,
      6
   )
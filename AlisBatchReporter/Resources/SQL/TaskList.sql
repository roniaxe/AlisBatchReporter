SELECT
   gba.batch_id as "Batch Id",	
   tt.task_id AS "Task Id",
   tt.task_name AS "Task",
   min(gba.entry_time) AS "Time",
   CASE WHEN (SELECT count(gba2.entry_type) 
			   FROM g_batch_audit gba2 
			   WHERE gba2.batch_run_num = gba.batch_run_num 
			   AND ((gba2.entry_type = 4 AND gba2.description LIKE '%terminated%')
			   OR (gba2.entry_type = 2 and gba2.description LIKE '%End of batch run%'))) > 0 
			   THEN 'Finished' 
			   ELSE 'Did Not Finish' end as "Status",
   tb.batch_name AS "Batch",
   gba.batch_run_num AS "Batch Run Number",
   (SELECT max(gba3.chunk_id) 
   FROM g_batch_audit gba3 
   WHERE gba3.batch_run_num = gba.batch_run_num) AS "Num Of Chunks"
FROM
   g_batch_audit gba 
   JOIN
      t_task tt 
      ON gba.task_id = tt.task_id 
   JOIN
      t_batch tb 
      ON gba.batch_id = tb.batch_id 
WHERE
   gba.ENTRY_TIME > {Param1} 
   AND gba.ENTRY_TIME - 1 <= {Param2} 
GROUP BY
   gba.batch_id,
   tb.batch_name,
   tt.task_name,
   tt.task_id,
   gba.batch_run_num
ORDER BY
   min(gba.entry_time)
   option(fast 100);
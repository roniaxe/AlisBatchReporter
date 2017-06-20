SELECT  
   tt.task_id AS "Task Id",
   tt.task_name AS "Task",
   min(gba.entry_time) AS "Time",
   tb.batch_name AS "Batch",
   gba.batch_run_num AS "Batch Run Number"
FROM
   g_batch_audit gba 
   JOIN
      t_task tt 
      ON gba.task_id = tt.task_id 
   JOIN
      t_batch tb 
      ON gba.batch_id = tb.batch_id 
WHERE
   gba.ENTRY_TIME > {fromDate} 
   AND gba.ENTRY_TIME - 1 <= {toDate} 
GROUP BY
   tb.batch_name,
   tt.task_name,
   tt.task_id,
   gba.batch_run_num
ORDER BY
   min(gba.entry_time);
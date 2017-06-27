SELECT
   iex.individual_identification_number 
FROM
   i_eft_export iex 
WHERE
   batch_run_no = 
   (
      SELECT
         TOP 1 iex.batch_run_no 
      FROM
         i_eft_export iex 
      ORDER BY
         pk DESC
   )
;
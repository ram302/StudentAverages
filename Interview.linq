<Query Kind="Program" />

void Main()
{
	const string fileName = @"c:\temp\scores.csv";
	String[] lines = File.ReadAllLines(fileName);
	//output the lines
	//lines.Dump();
	
	//todo calculate the average for each student and display the student’s name with the highest average.
	var studentData = from l in lines
						// Skip header row.
						.Skip(1)
						// Split by comma.
						let x = l.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
						// Parsed out into fields.
						select new
						{
							ID = Convert.ToInt32(x.ElementAt(0))
							, Name = x.ElementAt(1)
							, Subject = x.ElementAt(2)
							, Grade = Convert.ToInt32(x.ElementAt(3))
						};
	
	var studentAvgRank = from b in studentData
						// Grouping by name.
                    	.GroupBy(g => g.Name)
						// Interested in the averages for each students total scores.
                        .Select(g => new
                        {
							Name = g.Key
							, Avg = g.Average(c => c.Grade)
						})
						// In descending order.
						orderby b.Avg descending
						// We're only interested in the name, but just in case we can get the average too.
						select b;
	
	// Output only the name of the highest average student, so select name of first entry in sorted list.
	Console.WriteLine(studentAvgRank.First().Name);
}
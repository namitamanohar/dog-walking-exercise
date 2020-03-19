SELECT o.[name] AS 'Owner Name', n.[name] AS 'Neighborhood Name'
FROM Owner o
LEFT JOIN Neighborhood n ON 
o.NeighborhoodId =  n.Id 

SELECT o.[name] AS 'Owner Name', n.[name] AS 'Neighborhood Name'
FROM Owner o
LEFT JOIN Neighborhood n ON 
o.NeighborhoodId =  n.Id 
WHERE o.Id = 3 

SELECT w.[Name]
FROM Walker w
ORDER BY w.[Name] desc 

SELECT DISTINCT Breed
FROM Dog 

SELECT d.[name] AS 'Dog Name', o.[name] AS 'Owner Name', n.[name] AS 'Neighborhood Name'
FROM Dog d 
LEFT JOIN [Owner] o 
ON o.Id = d.OwnerId 
LEFT JOIN Neighborhood n 
ON n.Id =o.NeighborhoodId 

SELECT o.[name] As 'Owner Name', COUNT(OwnerId) AS 'Dog Count'
FROM Dog d 
LEFT JOIN [Owner] o 
ON o.Id = d.OwnerId
GROUP BY OwnerId, o.[name]

SELECT COUNT(WalkerId) As 'Walker Count', wa.[Name] AS 'Walker Name'
FROM Walks w 
LEFT JOIN Walker wa
ON w.WalkerId = wa.Id
GROUP BY WalkerId, wa.[Name]

SELECT COUNT(NeighborhoodId) As 'Walker Count', n.[name] AS 'Neighborhood Name'
FROM Walker w 
LEFT JOIN Neighborhood n
ON w.NeighborhoodId = n.Id
GROUP BY NeighborhoodId, n.[name]

SELECT d.[name] AS 'Dog Name'
FROM Dog d 
LEFT JOIN Walks w 
ON w.DogId = d.Id
WHERE w.[date] <'03/19/2020' and w.[date] >'03/12/2020'

SELECT d.[name] AS 'Dog Name'
FROM Dog d 
LEFT JOIN Walks w 
ON w.DogId = d.Id
GROUP BY d.[name]
HAVING COUNT(w.Id)=0 


# Cloud-API
Opdracht voor het vak Cloud API's This API will make it possible to search for excercises depending on which part of your body you want to train.

## Get list of Exercises

### Request
`GET /api/v1/exercises`

#### Filter by name
`GET /api/v1/exercises?name={exercice_name}`

#### Sort by name
`GET /api/v1/exercises?sortBy=name`

#### Sorting direction (default = desc)
`GET /api/v1/exercises?sortBy=name&direction=asc`

#### Paging (default = 0)
`GET /api/v1/exercises?page={page_number}`

#### Limit (default = 10)
`GET /api/v1/exercises?limit={max_items}`

### Response

```
HTTP/1.1 200 OK
Date: Thu, 24 Feb 2011 12:36:30 GMT
Status: 200 OK
Connection: close
Content-Type: application/json

[]
```

## Get a specific Exercise

### Request
`GET /api/v1/exercises/{id}`

### Response

```
HTTP/1.1 200 OK
Date: Thu, 24 Feb 2011 12:36:30 GMT
Status: 200 OK
Connection: close
Content-Type: application/json

{}
```

## Create an Exercise

### Request
`POST /api/v1/exercises`

### Response

```
HTTP/1.1 201 Created
Date: Thu, 24 Feb 2011 12:36:30 GMT
Status: 201 Created
Connection: close
Content-Type: application/json

{}
```

## Update an Exercise

### Request
`PUT /api/v1/exercises/{id}`

### Response

```
HTTP/1.1 200 OK
Date: Thu, 24 Feb 2011 12:36:30 GMT
Status: 200 OK
Connection: close
Content-Type: application/json

{}
```

## Delete an Exercise 

### Request 
`DELETE /api/v1/exercises/{id}`

### Response

```
HTTP/1.1 200 OK
Date: Thu, 24 Feb 2011 12:36:30 GMT
Status: 200 OK
Connection: close
Content-Type: application/json
```
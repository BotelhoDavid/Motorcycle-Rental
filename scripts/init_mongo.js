const DB_NAME = "Moto_Rental_Logs";
db = db.getSiblingDB(DB_NAME);

if (!db.getCollectionNames().includes("Logs")) {
    db.createCollection("Logs", {
        validator: {
            $jsonSchema: {
                bsonType: "object",
                required: ["Moto_id", "Plate", "CreatedDate", "Message"],
                properties: {
                    Moto_id: {
                        bsonType: "string",
                        description: "ID da moto (UUID ou string)"
                    },
                    Plate: {
                        bsonType: "string",
                        description: "Placa da moto"
                    },
                    CreatedDate: {
                        bsonType: "date",
                        description: "Data e hora do log (UTC)"
                    },
                    Message: {
                        bsonType: "string",
                        description: "Conteúdo da mensagem do log"
                    }
                }
            }
        }
    });
}

db.Logs.createIndex({ Moto_id: 1 }, { background: true });
db.Logs.createIndex({ Plate: 1 }, { background: true });
db.Logs.createIndex({ CreatedDate: -1 }, { background: true });

const {gql} = require('apollo-server');

module.exports = gql`
type Session{
    id: ID!,
    title: String!,
    description: String,
    startsAt: String,
    endsAt: String,
    room: String,
    day: String,
    format: String,
    track: String @deprecated(reason: "we will move on to a tag system later"),
    level: String,
    favorite: Boolean,
    speakers:[Speaker]
}
input SessionInput{
    title: String!,
    description: String,
    startsAt: String,
    endsAt: String,
    room: String,
    day: String,
    format: String,
    track: String,
    level: String,
    favorite: Boolean
}
type Speaker{
    id:ID!
    bio:String
    name:String
    sessions:[Session]
}
type Mutation{
    toggleFavoriteSession(id: ID): Session,
    addNewSession(session: SessionInput): Session
}
type Query{
    sessions(
        id: ID,
        title: String,
        description: String,
        startsAt: String,
        endsAt: String,
        room: Room,
        day: String,
        format: String,
        track: String,
        level: String
    ): [Session],
    sessionById(id:ID): SessionOrError
    speakers: [Speaker]
    speakerById(id: ID): Speaker
}

union SessionOrError = Session | Error

type Error{
    code: String
    message: String
    token: String
}

enum Room{
    EUROPA
    SOL
    SATURN
}
`;
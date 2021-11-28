export interface BusMessage {
    MessageName: string;
}

export interface BusResponse {}

export interface Handler<T extends BusMessage, S extends BusResponse> {
    Handle(message: T): Promise<S>
}

export interface ServiceBus {
    Dispatch<T extends BusMessage, S extends BusResponse>(message: T): Promise<S>; 
}

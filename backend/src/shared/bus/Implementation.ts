import { BusMessage, BusResponse, Handler, ServiceBus } from "./Abstractions";

export class InMemoryServiceBus implements ServiceBus {
    private readonly handlers: InMemoryHandlers;

    constructor() {
        this.handlers = new InMemoryHandlers();
    }

    public async Dispatch<T extends BusMessage, S extends BusResponse>(message: T): Promise<S> {
        const handler: Handler<T,S> = this.handlers.FindHandler(message.MessageName);
        const handlerResponse: S = await handler.Handle(message);
        return handlerResponse;
    }

    public Register<T extends BusMessage, S extends BusResponse>(handler: Handler<T,S>, handlerName: string): void {
        this.handlers.Add(handlerName, handler);
    }
}

export class InMemoryHandlers {
    private readonly registeredHandlers: Array<RegisteredInMemoryHandler<any,any>>;

    constructor() {
        this.registeredHandlers = [];
    }

    public Add<T extends BusMessage, S extends BusResponse>(handlerName: string, handler: Handler<T,S>): void {
        const registeredHandler = new RegisteredInMemoryHandler<T,S>(handlerName, handler);
        this.registeredHandlers.push(registeredHandler);
    }

    public FindHandler<T extends BusMessage, S extends BusResponse>(name: string): Handler<T,S> {
        const registeredHandler = this.registeredHandlers.find(r => r.Name === name);
        if (!registeredHandler) {
            throw new Error("".concat("handler ", name, " does not exist"));
        }
        return registeredHandler.Handler;
    }
}

export class RegisteredInMemoryHandler<T extends BusMessage, S extends BusResponse> {
    
    public get Name(): string {
        return this.name;
    }

    public get Handler(): Handler<T,S> {
        return this.handler;
    }

    constructor(
        private readonly name: string, 
        private readonly handler: Handler<T,S>
    ) {}
}
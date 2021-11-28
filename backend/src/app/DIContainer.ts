import { Container } from "inversify";

type DIRegister = (container: Container) => Container;

const registerDepencies = (registers: DIRegister[]): Container => {
    const container = new Container();
    registers.forEach(register => register(container));
    return container;
}

// TODO
export const DIContainer = registerDepencies([]);

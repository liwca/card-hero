﻿import { ICardCollectionModel, IStoreItemModel, StoreApiClient, StoreItemModel } from "../clients/clients";
import { AppBootstrap } from "../components/shared/AppBootstrap";

export class StoreService {
    static async getStoreItems(): Promise<IStoreItemModel[] | null> {
        const client = new StoreApiClient(AppBootstrap.baseUrl);
        const model = await client.get();

        return model;
    }

    static async buyCardBundle(id: number): Promise<ICardCollectionModel[] | null> {
        const client = new StoreApiClient(AppBootstrap.baseUrl);

        const postModel = new StoreItemModel({
            id: id
        });
        const model = await client.buyStoreItem(postModel);

        return model;
    }
}
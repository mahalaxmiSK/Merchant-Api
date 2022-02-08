import React, { Component} from 'react';
import { Input } from 'reactstrap';
import '../css/StyleSheet.css';

export class UpdateStock extends Component {

    static displayName = UpdateStock.name;
    constructor(props) {
        super(props);

        this.state = {
            productsNos: [],
            selectedProductNo:"",
            stockLocationId: 2,
            loading: true,
            submitResponse:""
        };
    }
    componentDidMount() {

        this.fetchProductsNo();
    }
    async fetchProductsNo() {
        const response = await fetch('updatestock/getproductsno');
        const data = await response.json();
        this.setState({ productsNos: data, loading: false });
        if (this.state.productsNos.length > 0) {
            this.state.selectedProductNo = this.state.productsNos[0];
        }
    }
    handleProductNoChange = (e) => {
        this.state.selectedProductNo = e.target.value;
    }
    handleStockLocationIdChange = (e) => {
        this.state.stockLocationId = e.target.value;
    }
    handleSubmit=() =>{
        this.submitStock();
        
    }
    async submitStock() {
        this.setState({ submitResponse: "Loading" });
        const response = await fetch('updatestock/updatestockto25/' + this.state.selectedProductNo + '/' + this.state.stockLocationId);
        console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ submitResponse: data.response });
    }
    render() {
        return (
            <div>
                <br />
                <h1 class="main-header-h1" style={{borderBottom: "2px solid lightgray", flex: 1 }}>
                    Select below options to update stock value to 25
            </h1>
                <br />
                <div class="row div-style">
                    <h1 class="sub-header-h1">Merchant Product No :</h1>

                    <Input type="select" 
                        defaultValue="Select" style={{ fontWeight: "normal", width: "150px" }}
                        onChange={(e) => this.handleProductNoChange(e)} >
                        {this.state.productsNos.map(order =>
                            <option>{order}</option>
                        )}
                    </Input>
                </div>
                <div class="row div-style">
                    <h1 class="sub-header-h1"> Stock Location Id :</h1>

                    <Input type="text" defaultValue="2" style={{ fontWeight: "normal", width: "150px", marginLeft: '35px'}}
                        onChange={(e) => this.handleStockLocationIdChange(e)}
                        onKeyPress={(event) => {
                            if (!/[0-9]/.test(event.key)) {
                                event.preventDefault();
                            }
                        }}>
                    </Input>
                </div>
                <div class="row div-style">
                    <input type="submit" value="Update Stock" onClick={this.handleSubmit} />
                </div>
                <h1>{this.state.submitResponse}</h1>
            </div>
        );
    }
}
